using HotelAIAgent.Domain.Common;
using HotelAIAgent.Domain.Enums;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Müşteri ile otel arasındaki bir sohbet oturumunu temsil eder.
/// State Machine ile yönetilen durumlar (Started → InProgress → OfferSent → Accepted/Rejected)
/// bu entity üzerinde tutulur.
/// </summary>
public class Conversation : BaseEntity
{
    /// <summary>Görüşmenin yapıldığı otelin ID'si.</summary>
    public Guid HotelId { get; set; }

    /// <summary>Görüşmeye katılan müşterinin ID'si.</summary>
    public Guid CustomerId { get; set; }

    /// <summary>Görüşmenin mevcut durumu (State Machine tarafından yönetilir).</summary>
    public ConversationState CurrentState { get; set; } = ConversationState.Started;

    /// <summary>Gelen son mesaj metni (referans için saklanır).</summary>
    public string? LastMessageText { get; set; }

    /// <summary>Sistemin verdiği son yanıt metni.</summary>
    public string? LastReplyText { get; set; }

    // Navigation Properties
    /// <summary>Görüşmenin yapıldığı otel.</summary>
    public Hotel Hotel { get; set; } = null!;

    /// <summary>Görüşmeye katılan müşteri.</summary>
    public Customer Customer { get; set; } = null!;

    /// <summary>Bu görüşme kapsamında sunulan teklifler.</summary>
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();

    /// <summary>Bu görüşmeye ait denetim günlükleri.</summary>
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
