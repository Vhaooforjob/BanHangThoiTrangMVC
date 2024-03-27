using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> AddToCart(int id, int quantity, ShoppingCart cart);
        Task<(CartCode, Order)> Checkout(OrderViewModel req, ShoppingCart cart, int voucherValue);
    }
}
