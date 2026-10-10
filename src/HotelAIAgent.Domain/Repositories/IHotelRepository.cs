using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// Hotel entity'si için özelleştirilmiş repository arayüzü.
/// </summary>
public interface IHotelRepository : IRepository<Hotel>
{
    /// <summary>Belirli bir Tenant'a ait tüm otelleri getirir.</summary>
    Task<List<Hotel>> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>Hotel'i ilişkili Rooms ve Rules ile birlikte getirir.</summary>
    Task<Hotel?> GetWithDetailsAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
