using BanHangThoiTrangMVC.Services.Interfaces;
using System;

public static class ServiceCollectionManager
{
    public static IServiceProvider RegisterServices(this IServiceProvider serviceProvider)
    {
        serviceProvider.GetService(typeof(IProductService));
        return serviceProvider;
    }
}