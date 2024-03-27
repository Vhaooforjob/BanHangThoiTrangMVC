using AutoMapper;
using BanHangThoiTrangMVC.MappingProfiles;

namespace BanHangThoiTrangMVC.ExtensionAndHelper
{
    public class MapperServiceCollection
    {
        public static MapperConfiguration Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(ProductMappingProfile));
                cfg.AddProfile(typeof(OrderMappingProfile));
                cfg.AddProfile(typeof(CategoryMappingProfile));
                cfg.AddProfile(typeof(ShoppingCartMappingProfile));
            });
            return mapperConfiguration;
        }
    }
}