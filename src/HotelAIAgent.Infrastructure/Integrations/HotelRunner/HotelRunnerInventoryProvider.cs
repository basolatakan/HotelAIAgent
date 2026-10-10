using HotelAIAgent.Application.Interfaces;
using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Infrastructure.Integrations.HotelRunner;

public class HotelRunnerInventoryProvider : IInventoryService
{
    public Task<List<Room>> GetAvailableRoomsAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        // TODO: Faz 2 veya 3'te gerçek HotelRunner API entegrasyonu buraya yazılacak.
        throw new NotSupportedException("Canlı HotelRunner entegrasyonu henüz MVP aşamasında aktif değildir. Lütfen InventoryProviderType.Mock kullanın.");
    }
}
