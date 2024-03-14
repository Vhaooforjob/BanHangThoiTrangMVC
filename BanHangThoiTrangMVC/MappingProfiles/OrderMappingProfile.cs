using AutoMapper;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;

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