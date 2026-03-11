using Graduation_Project.Domain.Entities;

namespace Graduation_Project.Domain.Interfaces;

public interface ISaleRepository : IGenericRepository<Sale>
{
    Task<IEnumerable<Sale>> GetSalesByUserAsync(string userId);
    Task<IEnumerable<Sale>> GetAllWithDetailsAsync();
    Task<IEnumerable<Sale>> GetPendingOrdersAsync();
    Task<decimal> GetTotalRevenueAsync();
    Task<IEnumerable<Sale>> GetSalesInDateRangeAsync(DateTime from, DateTime to);
}

