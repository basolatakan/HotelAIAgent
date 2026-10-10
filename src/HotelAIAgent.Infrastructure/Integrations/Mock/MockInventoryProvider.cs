using HotelAIAgent.Application.Interfaces;
using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Repositories;

namespace HotelAIAgent.Infrastructure.Integrations.Mock;

public class MockInventoryProvider : IInventoryService
{
    private readonly IRoomRepository _roomRepository;

    public MockInventoryProvider(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        // Mock senaryosu: Gerçek bir sistem yerine veritabanındaki 
        // IsAvailable = true olan odaları direkt olarak döner.
        return await _roomRepository.GetAvailableByHotelIdAsync(hotelId, cancellationToken);
    }
}
