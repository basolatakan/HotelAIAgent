using HotelAIAgent.Domain.Common;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Sistemdeki her önemli işlemin kaydını tutan denetim günlüğü entity'si.
/// Fiyat teklifleri, durum değişiklikleri, indirim kararları ve
/// satış ekibine devir gibi olaylar burada loglanır.
/// </summary>
public class AuditLog : BaseEntity
{
    /// <summary>Bu logun ait olduğu otelin ID'si.</summary>
    public Guid HotelId { get; set; }

    /// <summary>İlgili görüşmenin ID'si (varsa).</summary>
    public Guid? ConversationId { get; set; }

    /// <summary>Gerçekleştirilen işlem (Örn: "StateChanged", "DiscountApplied", "EscalatedToSales").</summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>İşlem detayları (JSON formatında: kural ID'si, hesaplanan indirim, vb.).</summary>
    public string Details { get; set; } = string.Empty;

    // Navigation Properties
    /// <summary>Bu logun ait olduğu otel.</summary>
    public Hotel Hotel { get; set; } = null!;

    /// <summary>İlgili görüşme (varsa).</summary>
    public Conversation? Conversation { get; set; }
}
