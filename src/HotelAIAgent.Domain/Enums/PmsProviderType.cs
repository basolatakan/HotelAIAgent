namespace HotelAIAgent.Domain.Enums;

/// <summary>
/// Otelin kullandığı PMS (Property Management System) sağlayıcı tipini belirtir.
/// IntegrationFactory bu enum'a bakarak doğru PMS implementasyonunu çözümler.
/// </summary>
public enum PmsProviderType
{
    /// <summary>Test/geliştirme için sahte veri sağlayıcısı.</summary>
    Mock = 0,

    /// <summary>Adonis PMS entegrasyonu.</summary>
    Adonis = 1,

    /// <summary>Opera PMS entegrasyonu.</summary>
    Opera = 2,

    /// <summary>Elektra PMS entegrasyonu.</summary>
    Elektra = 3,

    /// <summary>HMS (Hotel Management System) entegrasyonu.</summary>
    Hms = 4
}
