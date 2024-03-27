using AutoMapper;
using BanHangThoiTrangMVC.Areas.Admin.Controllers;
using BanHangThoiTrangMVC.Controllers;
using BanHangThoiTrangMVC.ExtensionAndHelper;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Repositories.Implement;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using BanHangThoiTrangMVC.Services.Implement;
using BanHangThoiTrangMVC.Services.Interfaces;
using BanHangThoiTrangMVC.Strategy.Sort;
using BanHangThoiTrangMVC.Strategy;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web;
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
            //container.RegisterType<UserManager<ApplicationUser>>(new InjectionConstructor());
            //container.RegisterType<ApplicationUserManager>(new TransientLifetimeManager());

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IShoppingCartService, ShoppingCartService>();

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();

            container.RegisterType<IUnitOfWorkRepository, UnitOfWorkRepository>();

            container.RegisterType<DbContext, ApplicationDbContext>(
                new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<ManageController>(new InjectionConstructor());
            //container.RegisterType<AccountController>(
            //    new InjectionConstructor());
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(
                    o => System.Web.HttpContext.Current.GetOwinContext().Authentication
                )
            );
            MapperConfiguration config = MapperServiceCollection.Configure();
            //build the mapper
            IMapper mapper = config.CreateMapper();
            container.RegisterType<IMapper, Mapper>();
            container.RegisterInstance(mapper);
            //container.RegisterType<IM>(mapper);

            container.RegisterType<Controller>(new TransientLifetimeManager());
            container.RegisterType<ApplicationDbContext>(new TransientLifetimeManager());
            //container.RegisterType<DbContext>(new TransientLifetimeManager());
            container.RegisterType<DbContext>(new TransientLifetimeManager(), new InjectionFactory(x => new ApplicationDbContext()));

            //SortProduct
            container.RegisterType<IProductSortingStrategy, DefaultProductSortingStrategy>();
            container.RegisterType<IProductSortingStrategy, PriceProductSortingStrategy>();
            container.RegisterType<IProductSortingStrategy, NameProductSortingStrategy>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}