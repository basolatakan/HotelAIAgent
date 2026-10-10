using HotelAIAgent.Domain.Enums;

namespace HotelAIAgent.Application.Services;

/// <summary>
/// Müşteriden gelen metni analiz edip bir ConversationEvent çıkaran basit servis.
/// MVP aşamasında basit kelime yakalama (keyword matching) ile çalışır.
/// </summary>
public class EventDetector
{
    public ConversationEvent DetectEvent(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return ConversationEvent.Unknown;

        var lowerMessage = message.ToLowerInvariant();

        // 1. Onaylama / Kabul etme
        if (lowerMessage.Contains("tamam") || 
            lowerMessage.Contains("kabul") || 
            lowerMessage.Contains("onaylıyorum") || 
            lowerMessage.Contains("rezerve et") || 
            lowerMessage.Contains("alıyorum"))
        {
            return ConversationEvent.OfferAccepted;
        }

        // 2. Reddetme / İptal
        if (lowerMessage.Contains("istemiyorum") || 
            lowerMessage.Contains("pahalı") || 
            lowerMessage.Contains("iptal") || 
            lowerMessage.Contains("vazgeçtim"))
        {
            return ConversationEvent.OfferRejected;
        }

        // 3. Pazarlık / İndirim Talebi
        if (lowerMessage.Contains("indirim") || 
            lowerMessage.Contains("pazarlık") || 
            lowerMessage.Contains("fiyatı düşür") || 
            lowerMessage.Contains("daha ucuz") || 
            lowerMessage.Contains("son fiyat"))
        {
            return ConversationEvent.BargainRequested;
        }

        // 4. Standart Oda / Fiyat Sorgulama (Geri kalan her şey şimdilik bunu tetikler)
        // Normalde daha karmaşık NLU (Doğal Dil Anlama) eklenebilir.
        return ConversationEvent.RoomInquiry;
    }
}
