using HotelAIAgent.Domain.Entities;

namespace HotelAIAgent.Domain.Repositories;

/// <summary>
/// AuditLog entity'si için özelleştirilmiş repository arayüzü.
/// </summary>
public interface IAuditLogRepository : IRepository<AuditLog>
{
    /// <summary>Belirli bir görüşmenin tüm denetim günlüklerini getirir.</summary>
    Task<List<AuditLog>> GetByConversationIdAsync(Guid conversationId, CancellationToken cancellationToken = default);

    /// <summary>Belirli bir otelin tüm denetim günlüklerini getirir.</summary>
    Task<List<AuditLog>> GetByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
}
