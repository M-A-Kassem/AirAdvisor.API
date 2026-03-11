using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;
using Graduation_Project.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Repositories;

public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Sale>> GetSalesByUserAsync(string userId)
        => await _dbSet.Include(s => s.Product)
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.PurchaseDate).ToListAsync();

    public async Task<IEnumerable<Sale>> GetAllWithDetailsAsync()
        => await _dbSet.Include(s => s.Product)
            .OrderByDescending(s => s.PurchaseDate).ToListAsync();

    public async Task<IEnumerable<Sale>> GetPendingOrdersAsync()
        => await _dbSet.Include(s => s.Product)
            .Where(s => s.Status == OrderStatus.Pending)
            .OrderBy(s => s.PurchaseDate).ToListAsync();

    public async Task<decimal> GetTotalRevenueAsync()
        => await _dbSet.Where(s => s.Status == OrderStatus.Accepted)
            .SumAsync(s => s.TotalPrice);

    public async Task<IEnumerable<Sale>> GetSalesInDateRangeAsync(DateTime from, DateTime to)
        => await _dbSet.Include(s => s.Product)
            .Where(s => s.PurchaseDate >= from && s.PurchaseDate <= to)
            .OrderByDescending(s => s.PurchaseDate).ToListAsync();
}

