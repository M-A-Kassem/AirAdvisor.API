using Graduation_Project.Domain.Entities;

namespace Graduation_Project.Domain.Interfaces;

public interface IChatMessageRepository : IGenericRepository<ChatMessage>
{
    Task<IEnumerable<ChatMessage>> GetByUserIdAsync(string userId);
}

