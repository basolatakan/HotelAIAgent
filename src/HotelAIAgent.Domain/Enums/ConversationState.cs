namespace HotelAIAgent.Domain.Enums;

/// <summary>
/// Bir müşteri görüşmesinin (sohbet oturumunun) olası durumlarını tanımlar.
/// State Machine bu enum'ı kullanarak geçerli durum geçişlerini kontrol eder.
/// </summary>
public enum ConversationState
{
    /// <summary>Görüşme yeni başladı, müşteri ilk mesajını attı.</summary>
    Started = 0,

    /// <summary>Sistem misafiri tanıdı ve oda araştırması yapıyor.</summary>
    InProgress = 1,

    /// <summary>Müşteriye bir fiyat teklifi sunuldu, yanıt bekleniyor.</summary>
    OfferSent = 2,

    /// <summary>Müşteri teklifi kabul etti.</summary>
    Accepted = 3,

    /// <summary>Müşteri teklifi reddetti veya görüşme sonlandı.</summary>
    Rejected = 4,

    /// <summary>Görüşme iptal edildi.</summary>
    Cancelled = 5,

    /// <summary>İndirim limiti doldu, satış ekibine devredildi.</summary>
    EscalatedToSales = 6
}
