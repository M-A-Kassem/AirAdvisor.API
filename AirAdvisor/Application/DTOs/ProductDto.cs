using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Application.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public double CoolingCapacity { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
}

public class CreateProductDto
{
    [Required, MaxLength(100)]
    public string Brand { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Model { get; set; } = string.Empty;

    [Required, Range(1, double.MaxValue)]
    public double CoolingCapacity { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required, Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}

public class UpdateProductDto
{
    [MaxLength(100)]
    public string? Brand { get; set; }

    [MaxLength(100)]
    public string? Model { get; set; }

    [Range(1, double.MaxValue)]
    public double? CoolingCapacity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal? Price { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int? StockQuantity { get; set; }
}

