using AutoMapper;
using BanHangThoiTrangMVC.Areas.Admin.Controllers;
using BanHangThoiTrangMVC.ExtensionAndHelper;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using BanHangThoiTrangMVC.Services.Implement;
using BanHangThoiTrangMVC.Services.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace BanHangThoiTrangMVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<ProductsController>(new InjectionConstructor());

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProductService, ProductService>();

            container.RegisterType<IProductRepository, ProductRepository>();


            MapperConfiguration config = MapperServiceCollection.Configure();
            //build the mapper
            IMapper mapper = config.CreateMapper();
            container.RegisterType<IMapper, Mapper>();
            container.RegisterInstance(mapper);
            //container.RegisterType<IM>(mapper);



            container.RegisterType<ApplicationDbContext>(new TransientLifetimeManager());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}