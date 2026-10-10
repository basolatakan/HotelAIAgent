namespace HotelAIAgent.Domain.Enums;

/// <summary>
/// Müşterinin mesajından çıkarılan olayları (event) temsil eder.
/// EventDetector bu enum değerlerini üretir, StateMachine bunları tüketir.
/// </summary>
public enum ConversationEvent
{
    /// <summary>Müşteri oda/fiyat sorguladı.</summary>
    RoomInquiry = 0,

    /// <summary>Sistem misafiri başarıyla tanıdı.</summary>
    CustomerIdentified = 1,

    /// <summary>Müşteri indirim veya pazarlık talep etti.</summary>
    BargainRequested = 2,

    /// <summary>Müşteri teklifi kabul etti.</summary>
    OfferAccepted = 3,

    /// <summary>Müşteri teklifi reddetti.</summary>
    OfferRejected = 4,

    /// <summary>Görüşme iptal edildi.</summary>
    ConversationCancelled = 5,

    /// <summary>Bilinmeyen veya tanımlanamayan mesaj.</summary>
    Unknown = 99
}
