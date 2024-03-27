using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using BanHangThoiTrangMVC.Repositories.Interfaces;

namespace BanHangThoiTrangMVC.Repositories.Implement
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}