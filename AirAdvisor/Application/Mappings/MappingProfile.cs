using AutoMapper;
using Graduation_Project.Application.DTOs;
using Graduation_Project.Domain.Entities;

namespace Graduation_Project.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, UpdateProductDto>().ReverseMap();

        CreateMap<RoomCalculation, RoomCalculationResponseDto>();

        CreateMap<Sale, SaleDto>()
            .ForMember(d => d.ProductBrand, opt => opt.MapFrom(s => s.Product.Brand))
            .ForMember(d => d.ProductModel, opt => opt.MapFrom(s => s.Product.Model))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<ChatMessage, ChatMessageDto>();
    }
}

