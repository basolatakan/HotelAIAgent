using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Application.Interfaces;

/// <summary>
/// Envanter (Kanal Yönetimi) sistemleriyle etkileşim kuracak servislerin arayüzü.
/// HotelRunner, Elektra vb. sistemler bu arayüzü uygular.
/// </summary>
public interface IInventoryService
{
    /// <summary>
    /// Belirtilen otel için anlık müsait odaları güncel fiyatlarıyla getirir.
    /// </summary>
    /// <param name="hotelId">Otel ID'si</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Müsait odalar listesi</returns>
    Task<List<Room>> GetAvailableRoomsAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
