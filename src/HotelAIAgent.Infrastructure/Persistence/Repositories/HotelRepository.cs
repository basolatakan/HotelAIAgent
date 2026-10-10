using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelAIAgent.Infrastructure.Persistence.Repositories;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Hotel>> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(h => h.TenantId == tenantId).ToListAsync(cancellationToken);
    }

    public async Task<Hotel?> GetWithDetailsAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(h => h.Rooms)
            .Include(h => h.Rules)
            .FirstOrDefaultAsync(h => h.Id == hotelId, cancellationToken);
    }
}
