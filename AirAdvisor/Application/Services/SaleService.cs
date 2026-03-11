using AutoMapper;
using Graduation_Project.Application.DTOs;
using Graduation_Project.Application.Interfaces;
using Graduation_Project.Domain.Entities;
using Graduation_Project.Domain.Interfaces;

namespace Graduation_Project.Application.Services;

public class SaleService : ISaleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SaleDto> CreateAsync(string userId, CreateSaleDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId)
            ?? throw new KeyNotFoundException("Product not found.");

        if (product.StockQuantity < dto.Quantity)
            throw new InvalidOperationException($"Insufficient stock. Available: {product.StockQuantity}");

        var sale = new Sale
        {
            UserId = userId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            TotalPrice = product.Price * dto.Quantity,
            PurchaseDate = DateTime.UtcNow,
            Status = OrderStatus.Pending
        };

        await _unitOfWork.Sales.AddAsync(sale);
        await _unitOfWork.SaveChangesAsync();

        sale.Product = product;
        return _mapper.Map<SaleDto>(sale);
    }

    public async Task<IEnumerable<SaleDto>> GetUserPurchasesAsync(string userId)
    {
        var sales = await _unitOfWork.Sales.GetSalesByUserAsync(userId);
        return _mapper.Map<IEnumerable<SaleDto>>(sales);
    }

    public async Task<IEnumerable<SaleDto>> GetAllAsync()
    {
        var sales = await _unitOfWork.Sales.GetAllWithDetailsAsync();
        return _mapper.Map<IEnumerable<SaleDto>>(sales);
    }

    public async Task<IEnumerable<SaleDto>> GetByUserAsync(string userId)
    {
        var sales = await _unitOfWork.Sales.GetSalesByUserAsync(userId);
        return _mapper.Map<IEnumerable<SaleDto>>(sales);
    }

    public async Task<IEnumerable<SaleDto>> GetPendingOrdersAsync()
    {
        var sales = await _unitOfWork.Sales.GetPendingOrdersAsync();
        return _mapper.Map<IEnumerable<SaleDto>>(sales);
    }

    public async Task<SaleDto?> AcceptOrderAsync(int saleId)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(saleId);
        if (sale is null) return null;
        if (sale.Status != OrderStatus.Pending)
            throw new InvalidOperationException($"Order is already {sale.Status}.");

        var product = await _unitOfWork.Products.GetByIdAsync(sale.ProductId);
        if (product is null)
            throw new KeyNotFoundException("Product not found.");
        if (product.StockQuantity < sale.Quantity)
            throw new InvalidOperationException($"Insufficient stock. Available: {product.StockQuantity}");

        product.StockQuantity -= sale.Quantity;
        _unitOfWork.Products.Update(product);

        sale.Status = OrderStatus.Accepted;
        _unitOfWork.Sales.Update(sale);
        await _unitOfWork.SaveChangesAsync();

        sale.Product = product;
        return _mapper.Map<SaleDto>(sale);
    }

    public async Task<SaleDto?> RejectOrderAsync(int saleId)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(saleId);
        if (sale is null) return null;
        if (sale.Status != OrderStatus.Pending)
            throw new InvalidOperationException($"Order is already {sale.Status}.");

        sale.Status = OrderStatus.Rejected;
        _unitOfWork.Sales.Update(sale);
        await _unitOfWork.SaveChangesAsync();

        var product = await _unitOfWork.Products.GetByIdAsync(sale.ProductId);
        sale.Product = product!;
        return _mapper.Map<SaleDto>(sale);
    }
}

