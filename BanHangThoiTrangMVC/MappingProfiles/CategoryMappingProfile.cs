using AutoMapper;
using BanHangThoiTrangMVC.Models.Category;
using BanHangThoiTrangMVC.Models.EF;

namespace BanHangThoiTrangMVC.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
        }
    }
}