using Graduation_Project.Application.DTOs.Analytics;
using Graduation_Project.Application.Interfaces;
using Graduation_Project.Domain.Interfaces;
using Graduation_Project.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Application.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var totalUsers = await _userManager.Users.CountAsync();
        var totalProducts = await _unitOfWork.Products.CountAsync();
        var totalSales = await _unitOfWork.Sales.CountAsync();
        var totalRevenue = await _unitOfWork.Sales.GetTotalRevenueAsync();

        var allSales = await _unitOfWork.Sales.GetAllWithDetailsAsync();
        var acceptedSales = allSales.Where(s => s.Status == Domain.Entities.OrderStatus.Accepted).ToList();
        var salesList = acceptedSales;

        var bestSelling = salesList
            .GroupBy(s => new { s.ProductId, s.Product.Brand, s.Product.Model })
            .Select(g => new BestSellingProductDto
            {
                ProductId = g.Key.ProductId,
                Brand = g.Key.Brand,
                Model = g.Key.Model,
                TotalQuantity = g.Sum(s => s.Quantity),
                TotalRevenue = g.Sum(s => s.TotalPrice)
            })
            .OrderByDescending(x => x.TotalQuantity)
            .Take(10)
            .ToList();

        var monthlySales = salesList
            .GroupBy(s => new { s.PurchaseDate.Year, s.PurchaseDate.Month })
            .Select(g => new MonthlySalesDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                SalesCount = g.Count(),
                Revenue = g.Sum(s => s.TotalPrice)
            })
            .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
            .ToList();

        var users = await _userManager.Users.ToListAsync();
        var topCustomers = salesList
            .GroupBy(s => s.UserId)
            .Select(g =>
            {
                var user = users.FirstOrDefault(u => u.Id == g.Key);
                return new TopCustomerDto
                {
                    UserId = g.Key,
                    FullName = user?.FullName ?? "Unknown",
                    Email = user?.Email ?? "Unknown",
                    PurchaseCount = g.Count(),
                    TotalSpent = g.Sum(s => s.TotalPrice)
                };
            })
            .OrderByDescending(x => x.TotalSpent)
            .Take(10)
            .ToList();

        return new DashboardDto
        {
            TotalUsers = totalUsers,
            TotalProducts = totalProducts,
            TotalSales = totalSales,
            TotalRevenue = totalRevenue,
            BestSellingProducts = bestSelling,
            SalesPerMonth = monthlySales,
            TopCustomers = topCustomers
        };
    }

    public async Task<IEnumerable<UserInfoDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var result = new List<UserInfoDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserInfoDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                Address = user.Address,
                Roles = roles
            });
        }

        return result;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}

