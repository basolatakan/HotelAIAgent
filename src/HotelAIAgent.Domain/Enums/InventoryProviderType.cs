namespace HotelAIAgent.Domain.Enums;

/// <summary>
/// Otelin kullandığı Envanter/Kanal Yöneticisi sağlayıcı tipini belirtir.
/// IntegrationFactory bu enum'a bakarak doğru Inventory implementasyonunu çözümler.
/// </summary>
public enum InventoryProviderType
{
    /// <summary>Test/geliştirme için sahte veri sağlayıcısı.</summary>
    Mock = 0,

    /// <summary>HotelRunner envanter entegrasyonu.</summary>
    HotelRunner = 1,

    /// <summary>Icibot envanter entegrasyonu.</summary>
    Icibot = 2,

    /// <summary>Elektra envanter entegrasyonu.</summary>
    Elektra = 3
}
