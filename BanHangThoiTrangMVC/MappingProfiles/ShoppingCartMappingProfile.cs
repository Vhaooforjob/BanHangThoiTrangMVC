using AutoMapper;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;

public class ShoppingCartMappingProfile : Profile
{
    public ShoppingCartMappingProfile()
    {
        CreateMap<Product, ShoppingCartItem>()
            .ForMember(dest => dest.ProductId, src => src.MapFrom(p => p.Id))
            .ForMember(dest => dest.ProductName, src => src.MapFrom(p => p.Title))
            .ForMember(dest => dest.TotalPrice, src => src.MapFrom(p => p.Quantity * p.Price))
            .ForMember(dest => dest.CategoryName, src => src.MapFrom(p => p.ProductCategory.Title));

        CreateMap<OrderDetail, ShoppingCartItem>();
    }
}