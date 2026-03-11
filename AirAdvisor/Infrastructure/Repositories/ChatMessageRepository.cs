using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;
using Graduation_Project.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Repositories;

public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<ChatMessage>> GetByUserIdAsync(string userId)
        => await _dbSet.Where(c => c.UserId == userId)
            .OrderByDescending(c => c.Timestamp).ToListAsync();
}

