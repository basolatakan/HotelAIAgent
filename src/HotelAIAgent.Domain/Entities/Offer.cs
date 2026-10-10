using HotelAIAgent.Domain.Common;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Müşteriye sunulan bir fiyat teklifini temsil eder.
/// Her teklif bir Conversation ve Room'a bağlıdır.
/// ValidUntil süresi geçmiş teklifler geçersiz sayılır.
/// </summary>
public class Offer : BaseEntity
{
    /// <summary>Bu teklifin ait olduğu görüşmenin ID'si.</summary>
    public Guid ConversationId { get; set; }

    /// <summary>Teklif edilen odanın ID'si.</summary>
    public Guid RoomId { get; set; }

    /// <summary>Odanın standart fiyatı (indirim öncesi).</summary>
    public decimal OriginalPrice { get; set; }

    /// <summary>Müşteriye sunulan nihai fiyat (indirim sonrası).</summary>
    public decimal OfferedPrice { get; set; }

    /// <summary>Uygulanan indirim yüzdesi.</summary>
    public decimal DiscountApplied { get; set; }

    /// <summary>Hangi kuralın uygulandığı (referans için).</summary>
    public Guid? AppliedRuleId { get; set; }

    /// <summary>Teklifin son geçerlilik tarihi (UTC). Bu tarihten sonra teklif geçersizdir.</summary>
    public DateTime ValidUntil { get; set; }

    /// <summary>Müşterinin teklifi kabul edip etmediği.</summary>
    public bool? IsAccepted { get; set; }

    // Navigation Properties
    /// <summary>Bu teklifin ait olduğu görüşme.</summary>
    public Conversation Conversation { get; set; } = null!;

    /// <summary>Teklif edilen oda.</summary>
    public Room Room { get; set; } = null!;
}
