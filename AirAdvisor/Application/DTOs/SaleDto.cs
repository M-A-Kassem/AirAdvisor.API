using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Application.DTOs;

public class SaleDto
{
    public int SaleId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string ProductBrand { get; set; } = string.Empty;
    public string ProductModel { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateSaleDto
{
    [Required]
    public int ProductId { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}

