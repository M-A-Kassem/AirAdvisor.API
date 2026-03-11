namespace Graduation_Project.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IRoomCalculationRepository RoomCalculations { get; }
    ISaleRepository Sales { get; }
    IChatMessageRepository ChatMessages { get; }
    Task<int> SaveChangesAsync();
}

