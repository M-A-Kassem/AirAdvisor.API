using Graduation_Project.Application.DTOs;

namespace Graduation_Project.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ProductDto>> GetByBrandAsync(string brand);
    Task<IEnumerable<ProductDto>> GetRecommendedForCapacityAsync(double coolingLoad);
}

