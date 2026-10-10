using HotelAIAgent.Domain.Common;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Otelden hizmet alan misafiri temsil eder.
/// Telefon numarası ile benzersiz olarak tanımlanır.
/// PMS entegrasyonu üzerinden geçmiş konaklama bilgileri çekilir.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>Müşteri telefon numarası (Benzersiz anahtar, Örn: "+905554443322").</summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>Müşterinin tam adı.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>Müşterinin e-posta adresi (opsiyonel).</summary>
    public string? Email { get; set; }

    // Navigation Properties
    /// <summary>Bu müşterinin katıldığı görüşmeler.</summary>
    public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}
