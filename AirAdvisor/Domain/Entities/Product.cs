namespace Graduation_Project.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public double CoolingCapacity { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StockQuantity { get; set; }

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}

