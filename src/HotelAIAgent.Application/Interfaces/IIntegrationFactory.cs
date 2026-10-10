using HotelAIAgent.Domain.Enums;

namespace HotelAIAgent.Application.Interfaces;

/// <summary>
/// Aktif otelin provider ayarlarına göre doğru PMS veya Inventory servisini
/// çözümleyecek olan fabrika arayüzü.
/// </summary>
public interface IIntegrationFactory
{
    /// <summary>Belirtilen PMS sağlayıcı tipine uygun IPmsService uygulamasını döndürür.</summary>
    IPmsService GetPmsService(PmsProviderType providerType);

    /// <summary>Belirtilen Envanter sağlayıcı tipine uygun IInventoryService uygulamasını döndürür.</summary>
    IInventoryService GetInventoryService(InventoryProviderType providerType);
}
