using Graduation_Project.Application.DTOs;

namespace Graduation_Project.Application.Interfaces;

public interface ISaleService
{
    Task<SaleDto> CreateAsync(string userId, CreateSaleDto dto);
    Task<IEnumerable<SaleDto>> GetUserPurchasesAsync(string userId);
    Task<IEnumerable<SaleDto>> GetAllAsync();
    Task<IEnumerable<SaleDto>> GetByUserAsync(string userId);
    Task<IEnumerable<SaleDto>> GetPendingOrdersAsync();
    Task<SaleDto?> AcceptOrderAsync(int saleId);
    Task<SaleDto?> RejectOrderAsync(int saleId);
}

