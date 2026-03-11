using AutoMapper;
using Graduation_Project.Application.DTOs;
using Graduation_Project.Application.Interfaces;
using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;

namespace Graduation_Project.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        return product is null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var Oldproduct = await _unitOfWork.Products.GetByIdAsync(id);
        if (Oldproduct == null)
        {
            throw new Exception($"Product with id {id} not found");
        }


        _unitOfWork.Products.Update(Oldproduct);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDto>(Oldproduct);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product is null) return false;

        _unitOfWork.Products.Remove(product);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ProductDto>> GetByBrandAsync(string brand)
    {
        var products = await _unitOfWork.Products.GetProductsByBrandAsync(brand);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<IEnumerable<ProductDto>> GetRecommendedForCapacityAsync(double coolingLoad)
    {
        var products = await _unitOfWork.Products.GetProductsByCoolingCapacityAsync(coolingLoad);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}

