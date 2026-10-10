using HotelAIAgent.Domain.Common;
using HotelAIAgent.Domain.Enums;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Otel admininin CMS üzerinden tanımladığı indirim kuralını temsil eder.
/// RuleEngine, bu kuralları değerlendirerek müşteriye uygulanabilecek
/// maksimum indirim yüzdesini hesaplar.
/// </summary>
public class Rule : BaseEntity
{
    /// <summary>Bu kuralın ait olduğu otelin ID'si.</summary>
    public Guid HotelId { get; set; }

    /// <summary>Kural tipi (Konaklama sayısı, sezon, segment vb.).</summary>
    public RuleType RuleType { get; set; }

    /// <summary>Koşul operatörü (Örn: "GreaterThanOrEqual", "Equals", "LessThan").</summary>
    public string ConditionOperator { get; set; } = string.Empty;

    /// <summary>Koşul değeri (Örn: "3" konaklama sayısı veya "VIP" segment).</summary>
    public string ConditionValue { get; set; } = string.Empty;

    /// <summary>Kural eşleştiğinde uygulanacak maksimum indirim yüzdesi.</summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>Bu kural ile oluşturulan teklifin geçerlilik süresi (saat).</summary>
    public int OfferValidityHours { get; set; } = 24;

    /// <summary>Kuralın aktif olup olmadığı. Pasif kurallar değerlendirilmez.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Kuralın önceliği. Düşük sayı = yüksek öncelik.</summary>
    public int Priority { get; set; } = 100;

    // Navigation Properties
    /// <summary>Bu kuralın ait olduğu otel.</summary>
    public Hotel Hotel { get; set; } = null!;
}
