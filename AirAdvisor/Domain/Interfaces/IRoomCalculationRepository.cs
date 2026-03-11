using Graduation_Project.Domain.Entities;

namespace Graduation_Project.Domain.Interfaces;

public interface IRoomCalculationRepository : IGenericRepository<RoomCalculation>
{
    Task<IEnumerable<RoomCalculation>> GetByUserIdAsync(string userId);
}

