using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelAIAgent.Infrastructure.Persistence.Repositories;

public class RuleRepository : Repository<Rule>, IRuleRepository
{
    public RuleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Rule>> GetActiveRulesByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.HotelId == hotelId && r.IsActive)
            .OrderBy(r => r.Priority)
            .ToListAsync(cancellationToken);
    }
}
