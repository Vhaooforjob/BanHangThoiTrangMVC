using AutoMapper;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;

namespace BanHangThoiTrangMVC.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            CreateMap<OrderDetail, OrderDetailViewModel>();
            CreateMap<OrderDetailViewModel, OrderDetail>();
        }
    }
}