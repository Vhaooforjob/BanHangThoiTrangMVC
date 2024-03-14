using AutoMapper;

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
            });
            return mapperConfiguration;
        }
    }
}