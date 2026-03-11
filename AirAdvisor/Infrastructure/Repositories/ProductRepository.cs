using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;
using Graduation_Project.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand)
        => await _dbSet.Where(p => p.Brand.ToLower() == brand.ToLower()).ToListAsync();

    public async Task<IEnumerable<Product>> GetProductsByCoolingCapacityAsync(double minCapacity)
        => await _dbSet.Where(p => p.CoolingCapacity >= minCapacity)
            .OrderBy(p => p.CoolingCapacity).ToListAsync();
}

