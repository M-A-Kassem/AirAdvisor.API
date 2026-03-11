namespace Graduation_Project.Application.DTOs.Analytics;

public class DashboardDto
{
    public int TotalUsers { get; set; }
    public int TotalProducts { get; set; }
    public int TotalSales { get; set; }
    public decimal TotalRevenue { get; set; }
    public IEnumerable<BestSellingProductDto> BestSellingProducts { get; set; }
        = Enumerable.Empty<BestSellingProductDto>();
    public IEnumerable<MonthlySalesDto> SalesPerMonth { get; set; }
        = Enumerable.Empty<MonthlySalesDto>();
    public IEnumerable<TopCustomerDto> TopCustomers { get; set; }
        = Enumerable.Empty<TopCustomerDto>();
}

public class BestSellingProductDto
{
    public int ProductId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int TotalQuantity { get; set; }
    public decimal TotalRevenue { get; set; }
}

public class MonthlySalesDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int SalesCount { get; set; }
    public decimal Revenue { get; set; }
}

public class TopCustomerDto
{
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int PurchaseCount { get; set; }
    public decimal TotalSpent { get; set; }
}

public class UserInfoDto
{
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
}

