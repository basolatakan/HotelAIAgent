namespace HotelAIAgent.Domain.Enums;

/// <summary>
/// İndirim kuralının hangi koşula dayandığını tanımlar.
/// Her otel kendi kurallarını bu tiplere göre oluşturabilir.
/// </summary>
public enum RuleType
{
    /// <summary>Müşterinin geçmiş konaklama sayısına dayalı kural.</summary>
    RepeatVisitCount = 0,

    /// <summary>Sezonsal (yaz, kış, bayram vb.) indirim kuralı.</summary>
    Season = 1,

    /// <summary>Haftanın gününe dayalı indirim kuralı.</summary>
    DayOfWeek = 2,

    /// <summary>Müşteri segmentine (VIP, Corporate vb.) dayalı kural.</summary>
    CustomerSegment = 3,

    /// <summary>Erken rezervasyon indirim kuralı.</summary>
    EarlyBooking = 4
}
