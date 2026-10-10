using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// Offer entity'si için özelleştirilmiş repository arayüzü.
/// </summary>
public interface IOfferRepository : IRepository<Offer>
{
    /// <summary>Belirli bir görüşmeye ait tüm teklifleri getirir.</summary>
    Task<List<Offer>> GetByConversationIdAsync(Guid conversationId, CancellationToken cancellationToken = default);
}
