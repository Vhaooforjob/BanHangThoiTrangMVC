using BanHangThoiTrangMVC.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //List<Product> GetAll();
        Task<Product> GetByIdAndCountAsync(int id);
    }
}
