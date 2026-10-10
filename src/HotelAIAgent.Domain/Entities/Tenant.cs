using HotelAIAgent.Domain.Common;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Sistemdeki ana hesabı / aboneyi temsil eder (Örn: "Kaya Turizm Grubu").
/// Bir Tenant'ın altında birden fazla Hotel (şube) bulunabilir.
/// </summary>
public class Tenant : BaseEntity
{
    /// <summary>Tenant (otel sahibi / grup) adı.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>İletişim e-posta adresi.</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>İletişim telefon numarası.</summary>
    public string Phone { get; set; } = string.Empty;

    // Navigation Properties
    /// <summary>Tenant'a ait oteller/şubeler.</summary>
    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
