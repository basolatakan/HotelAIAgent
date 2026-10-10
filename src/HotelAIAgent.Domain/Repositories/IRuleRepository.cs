using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// Rule entity'si için özelleştirilmiş repository arayüzü.
/// </summary>
public interface IRuleRepository : IRepository<Rule>
{
    /// <summary>Belirli bir otelin tüm aktif kurallarını öncelik sırasına göre getirir.</summary>
    Task<List<Rule>> GetActiveRulesByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
