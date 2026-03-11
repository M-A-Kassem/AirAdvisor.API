using Graduation_Project.Domain.Entities;

namespace Graduation_Project.Domain.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand);
    Task<IEnumerable<Product>> GetProductsByCoolingCapacityAsync(double minCapacity);
}

