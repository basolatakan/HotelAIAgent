using HotelAIAgent.Application.Interfaces;
using HotelAIAgent.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace HotelAIAgent.Application.Services;

public class IntegrationFactory : IIntegrationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public IntegrationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IPmsService GetPmsService(PmsProviderType providerType)
    {
        // DI Container'dan ilgili implementasyonu resolve eder.
        // Bunu yapabilmek için ilgili servislerin "Keyed" (Named) registration ile kaydedilmesi
        // veya factory pattern ile IServiceProvider üzerinden alınması gerekir.
        // .NET 8 Keyed Services özelliğini kullanıyoruz.

        var service = _serviceProvider.GetKeyedService<IPmsService>(providerType);
        if (service == null)
            throw new NotSupportedException($"PMS Provider Type {providerType} is not registered.");

        return service;
    }

    public IInventoryService GetInventoryService(InventoryProviderType providerType)
    {
        var service = _serviceProvider.GetKeyedService<IInventoryService>(providerType);
        if (service == null)
            throw new NotSupportedException($"Inventory Provider Type {providerType} is not registered.");

        return service;
    }
}
