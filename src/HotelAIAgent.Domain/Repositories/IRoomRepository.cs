using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// Room entity'si için özelleştirilmiş repository arayüzü.
/// </summary>
public interface IRoomRepository : IRepository<Room>
{
    /// <summary>Belirli bir otelin tüm odalarını getirir.</summary>
    Task<List<Room>> GetByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);

    /// <summary>Belirli bir otelin müsait (satışta olan) odalarını getirir.</summary>
    Task<List<Room>> GetAvailableByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
