using AutoMapper;
using BanHangThoiTrangMVC.Models.EF;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile() 
    {
        CreateMap<Product, ProductViewModel>();
        CreateMap<ProductImageViewModel, Product>();

        CreateMap<ProductImage, ProductImageViewModel>();
        CreateMap<ProductImageViewModel, ProductImage>();

        CreateMap<ProductCategory, ProductCategoryViewModel>();
        CreateMap<ProductCategoryViewModel, ProductCategory>();
    }
}