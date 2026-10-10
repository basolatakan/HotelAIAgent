using HotelAIAgent.Domain.Entities;
using HotelAIAgent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelAIAgent.Infrastructure.Persistence.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Room>> GetByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(r => r.HotelId == hotelId).ToListAsync(cancellationToken);
    }

    public async Task<List<Room>> GetAvailableByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(r => r.HotelId == hotelId && r.IsAvailable).ToListAsync(cancellationToken);
    }
}
