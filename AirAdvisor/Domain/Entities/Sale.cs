namespace Graduation_Project.Domain.Entities;

public enum OrderStatus
{
    Pending = 0,
    Accepted = 1,
    Rejected = 2
}

public class Sale
{
    public int SaleId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public Product Product { get; set; } = null!;
}

