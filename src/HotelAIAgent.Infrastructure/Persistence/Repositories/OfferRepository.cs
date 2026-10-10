using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelAIAgent.Infrastructure.Persistence.Repositories;

public class OfferRepository : Repository<Offer>, IOfferRepository
{
    public OfferRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Offer>> GetByConversationIdAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.ConversationId == conversationId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
