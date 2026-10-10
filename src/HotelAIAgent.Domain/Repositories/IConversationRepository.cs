using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// Conversation entity'si için özelleştirilmiş repository arayüzü.
/// </summary>
public interface IConversationRepository : IRepository<Conversation>
{
    /// <summary>Belirli bir oteldeki tüm görüşmeleri getirir.</summary>
    Task<List<Conversation>> GetByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);

    /// <summary>Conversation'ı Customer, Offers ve AuditLogs ile birlikte getirir.</summary>
    Task<Conversation?> GetWithDetailsAsync(Guid conversationId, CancellationToken cancellationToken = default);

    /// <summary>Bir müşterinin belirli bir oteldeki aktif (henüz sonlanmamış) görüşmesini getirir.</summary>
    Task<Conversation?> GetActiveByCustomerAndHotelAsync(Guid customerId, Guid hotelId, CancellationToken cancellationToken = default);
}
