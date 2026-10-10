using HotelAIAgent.Domain.Common;
using HotelAIAgent.Domain.Enums;

namespace HotelAIAgent.Domain.Entities;

/// <summary>
/// Fiziksel bir otel veya şubeyi temsil eder.
/// Bir Tenant'ın birden fazla Hotel'i olabilir; her Hotel'in kendi odaları,
/// kuralları ve entegrasyon ayarları vardır.
/// </summary>
public class Hotel : BaseEntity
{
    /// <summary>Bu otelin ait olduğu Tenant'ın ID'si.</summary>
    public Guid TenantId { get; set; }

    /// <summary>Otel adı (Örn: "Kaya Belek Resort").</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Otel adresi.</summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>Otelin kullandığı PMS sağlayıcısı.</summary>
    public PmsProviderType PmsProvider { get; set; } = PmsProviderType.Mock;

    /// <summary>Otelin kullandığı envanter/kanal yöneticisi sağlayıcısı.</summary>
    public InventoryProviderType InventoryProvider { get; set; } = InventoryProviderType.Mock;

    /// <summary>
    /// Admin API erişimi için basit API anahtarı.
    /// İleride JWT/Identity ile değiştirilecek.
    /// </summary>
    public string AdminApiKey { get; set; } = string.Empty;

    // Navigation Properties
    /// <summary>Bu otele ait olan Tenant.</summary>
    public Tenant Tenant { get; set; } = null!;

    /// <summary>Oteldeki odalar.</summary>
    public ICollection<Room> Rooms { get; set; } = new List<Room>();

    /// <summary>Otelin tanımladığı indirim kuralları.</summary>
    public ICollection<Rule> Rules { get; set; } = new List<Rule>();

    /// <summary>Bu otelde gerçekleşen görüşmeler.</summary>
    public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}
