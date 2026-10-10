using HotelAIAgent.Application.Dtos;
using HotelAIAgent.Application.Interfaces;
using HotelAIAgent.Domain.Common;
using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Enums;
using HotelAIAgent.Domain.Repositories;

namespace HotelAIAgent.Application.Services;

public class AgentOrchestrator
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly EventDetector _eventDetector;
    private readonly StateMachine _stateMachine;
    private readonly RuleEngine _ruleEngine;
    private readonly IIntegrationFactory _integrationFactory;

    public AgentOrchestrator(
        IHotelRepository hotelRepository,
        IConversationRepository conversationRepository,
        ICustomerRepository customerRepository,
        IOfferRepository offerRepository,
        IAuditLogRepository auditLogRepository,
        IUnitOfWork unitOfWork,
        EventDetector eventDetector,
        StateMachine stateMachine,
        RuleEngine ruleEngine,
        IIntegrationFactory integrationFactory)
    {
        _hotelRepository = hotelRepository;
        _conversationRepository = conversationRepository;
        _customerRepository = customerRepository;
        _offerRepository = offerRepository;
        _auditLogRepository = auditLogRepository;
        _unitOfWork = unitOfWork;
        _eventDetector = eventDetector;
        _stateMachine = stateMachine;
        _ruleEngine = ruleEngine;
        _integrationFactory = integrationFactory;
    }

    public async Task<MessageResponseDto> ProcessMessageAsync(Guid hotelId, MessageRequestDto request, CancellationToken cancellationToken = default)
    {
        // 1. Otel bilgilerini al
        var hotel = await _hotelRepository.GetWithDetailsAsync(hotelId, cancellationToken);
        if (hotel == null) throw new Exception("Hotel not found.");

        // 2. Müşteri bilgilerini çöz (Yoksa oluştur)
        var customer = await _customerRepository.GetByPhoneNumberAsync(request.CustomerPhone, cancellationToken);
        if (customer == null)
        {
            customer = new Customer { PhoneNumber = request.CustomerPhone, FullName = request.CustomerName };
            await _customerRepository.AddAsync(customer, cancellationToken);
        }

        // 3. Conversation'ı çöz (Yoksa oluştur)
        Conversation conversation;
        if (request.ConversationId.HasValue)
        {
            conversation = await _conversationRepository.GetWithDetailsAsync(request.ConversationId.Value, cancellationToken)
                ?? throw new Exception("Conversation not found.");
        }
        else
        {
            conversation = await _conversationRepository.GetActiveByCustomerAndHotelAsync(customer.Id, hotel.Id, cancellationToken);
            if (conversation == null)
            {
                conversation = new Conversation { HotelId = hotel.Id, CustomerId = customer.Id, CurrentState = ConversationState.Started };
                await _conversationRepository.AddAsync(conversation, cancellationToken);
            }
        }

        conversation.LastMessageText = request.MessageText;

        // 4. Mesajdan olayı tespit et
        var detectedEvent = _eventDetector.DetectEvent(request.MessageText);

        // 5. Durumu ilerlet
        var previousState = conversation.CurrentState;
        conversation.CurrentState = _stateMachine.GetNextState(conversation.CurrentState, detectedEvent);

        if (previousState != conversation.CurrentState)
        {
            await LogAuditAsync(hotel.Id, conversation.Id, $"StateChanged", $"Durum değişti: {previousState} -> {conversation.CurrentState}");
        }

        // 6. İş Mantığı ve Rule Engine İşletimi
        var response = new MessageResponseDto { ConversationId = conversation.Id, CurrentState = conversation.CurrentState.ToString() };
        
        var pmsService = _integrationFactory.GetPmsService(hotel.PmsProvider);
        var inventoryService = _integrationFactory.GetInventoryService(hotel.InventoryProvider);

        if (conversation.CurrentState == ConversationState.InProgress)
        {
            // Oda sorgusu yapılıyor, ilk teklif oluşturulacak.
            var availableRooms = await inventoryService.GetAvailableRoomsAsync(hotel.Id, cancellationToken);
            var roomToOffer = availableRooms.FirstOrDefault(); // MVP'de bulduğumuz ilk odayı teklif ediyoruz

            if (roomToOffer != null)
            {
                var pastVisits = await pmsService.GetCustomerPastVisitCountAsync(hotel.Id, customer.PhoneNumber, cancellationToken);
                var ruleResult = await _ruleEngine.CalculateOfferAsync(hotel.Id, roomToOffer, roomToOffer.BasePrice, pastVisits, cancellationToken);

                var newOffer = new Offer
                {
                    ConversationId = conversation.Id,
                    RoomId = roomToOffer.Id,
                    OriginalPrice = roomToOffer.BasePrice,
                    OfferedPrice = ruleResult.OfferedPrice,
                    DiscountApplied = ruleResult.DiscountApplied,
                    AppliedRuleId = ruleResult.AppliedRuleId,
                    ValidUntil = DateTime.UtcNow.AddHours(24) // Sabit 24 saat
                };
                await _offerRepository.AddAsync(newOffer, cancellationToken);
                conversation.CurrentState = ConversationState.OfferSent;
                
                await LogAuditAsync(hotel.Id, conversation.Id, "OfferCreated", $"Yeni teklif oluşturuldu. Fiyat: {newOffer.OfferedPrice}");

                response.ReplyText = $"Merhaba! {roomToOffer.RoomType} odamız için sizin adınıza hesaplanmış gecelik fiyatımız {newOffer.OfferedPrice} TL'dir.";
                response.OfferedPrice = newOffer.OfferedPrice;
                response.CurrentState = conversation.CurrentState.ToString();
            }
            else
            {
                response.ReplyText = "Maalesef şu an müsait odamız bulunmamaktadır.";
                conversation.CurrentState = ConversationState.Rejected; // Başarısız sonlanma
            }
        }
        else if (conversation.CurrentState == ConversationState.OfferSent && detectedEvent == ConversationEvent.BargainRequested)
        {
            // Pazarlık durumu - Daha derin bir kural hesabı çalıştırılır. MVP'de tekrar aynı hesaplama veya farklı kurallar çağrılabilir.
            // Şimdilik sistem aynı hesaplamayı yapıp sınır uyarısını (Escalated) kontrol edecek.
            var activeOffer = conversation.Offers.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
            if (activeOffer != null && activeOffer.Room != null)
            {
                var pastVisits = await pmsService.GetCustomerPastVisitCountAsync(hotel.Id, customer.PhoneNumber, cancellationToken);
                
                // Pazarlıkta genelde mevcut teklif üzerinden veya yine BasePrice üzerinden ek kurallar taranabilir.
                // Biz doğrudan RuleEngine'e tekrar soruyoruz:
                var ruleResult = await _ruleEngine.CalculateOfferAsync(hotel.Id, activeOffer.Room, activeOffer.OfferedPrice, pastVisits, cancellationToken);

                if (ruleResult.Escalated || ruleResult.OfferedPrice >= activeOffer.OfferedPrice)
                {
                    // FloorPrice sınırına çarpıldı veya daha fazla indirim yok
                    await LogAuditAsync(hotel.Id, conversation.Id, "EscalatedToSales", $"İndirim limiti veya FloorPrice engeli. Mevcut teklif: {activeOffer.OfferedPrice}");
                    response.ReplyText = $"Sizin için sunabileceğim en son fiyat {activeOffer.OfferedPrice} TL'dir. Daha fazla indirim yetkim bulunmuyor. Satış ekibimize konuyu iletiyorum.";
                    response.OfferedPrice = activeOffer.OfferedPrice;
                }
                else
                {
                    // Yeni bir indirimli teklif yaratıldı
                    var newOffer = new Offer
                    {
                        ConversationId = conversation.Id,
                        RoomId = activeOffer.RoomId,
                        OriginalPrice = activeOffer.Room.BasePrice,
                        OfferedPrice = ruleResult.OfferedPrice,
                        DiscountApplied = ruleResult.DiscountApplied,
                        AppliedRuleId = ruleResult.AppliedRuleId,
                        ValidUntil = DateTime.UtcNow.AddHours(24)
                    };
                    await _offerRepository.AddAsync(newOffer, cancellationToken);
                    await LogAuditAsync(hotel.Id, conversation.Id, "BargainAccepted", $"Pazarlık sonucu yeni teklif: {newOffer.OfferedPrice}");

                    response.ReplyText = $"Peki, madem ısrar ettiniz sizin için fiyatı {newOffer.OfferedPrice} TL yapıyorum.";
                    response.OfferedPrice = newOffer.OfferedPrice;
                }
            }
        }
        else if (conversation.CurrentState == ConversationState.Accepted)
        {
            response.ReplyText = "Harika! Teklifimizi kabul ettiğiniz için teşekkürler. Ödeme linkini birazdan iletiyoruz.";
            await LogAuditAsync(hotel.Id, conversation.Id, "OfferAccepted", "Müşteri teklifi kabul etti.");
        }
        else if (conversation.CurrentState == ConversationState.Rejected)
        {
            response.ReplyText = "Teklifimizi uygun bulmadığınız için üzgünüz. Başka bir zaman tekrar görüşmek dileğiyle.";
            await LogAuditAsync(hotel.Id, conversation.Id, "OfferRejected", "Müşteri teklifi reddetti.");
        }
        else
        {
            response.ReplyText = "Sizi tam olarak anlayamadım, tekrar edebilir misiniz?";
        }

        conversation.LastReplyText = response.ReplyText;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }

    private async Task LogAuditAsync(Guid hotelId, Guid conversationId, string action, string details)
    {
        var log = new AuditLog
        {
            HotelId = hotelId,
            ConversationId = conversationId,
            Action = action,
            Details = details
        };
        await _auditLogRepository.AddAsync(log);
    }
}
