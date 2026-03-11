using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;
using Graduation_Project.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Repositories;

public class RoomCalculationRepository : GenericRepository<RoomCalculation>, IRoomCalculationRepository
{
    public RoomCalculationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<RoomCalculation>> GetByUserIdAsync(string userId)
        => await _dbSet.Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CalculatedAt).ToListAsync();
}

