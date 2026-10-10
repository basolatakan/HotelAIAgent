using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Enums;
using HotelAIAgent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelAIAgent.Infrastructure.Persistence.Repositories;

public class ConversationRepository : Repository<Conversation>, IConversationRepository
{
    public ConversationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Conversation>> GetByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(c => c.HotelId == hotelId).ToListAsync(cancellationToken);
    }

    public async Task<Conversation?> GetWithDetailsAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Customer)
            .Include(c => c.Offers)
            .Include(c => c.AuditLogs)
            .FirstOrDefaultAsync(c => c.Id == conversationId, cancellationToken);
    }

    public async Task<Conversation?> GetActiveByCustomerAndHotelAsync(Guid customerId, Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.CustomerId == customerId && c.HotelId == hotelId)
            .Where(c => c.CurrentState != ConversationState.Accepted 
                     && c.CurrentState != ConversationState.Rejected 
                     && c.CurrentState != ConversationState.Cancelled)
            .OrderByDescending(c => c.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
