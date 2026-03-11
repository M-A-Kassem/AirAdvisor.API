using Graduation_Project.Domain.Interfaces;
using Graduation_Project.Infrastructure.Data;

namespace Graduation_Project.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IProductRepository Products { get; }
    public IRoomCalculationRepository RoomCalculations { get; }
    public ISaleRepository Sales { get; }
    public IChatMessageRepository ChatMessages { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Products = new ProductRepository(context);
        RoomCalculations = new RoomCalculationRepository(context);
        Sales = new SaleRepository(context);
        ChatMessages = new ChatMessageRepository(context);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}

