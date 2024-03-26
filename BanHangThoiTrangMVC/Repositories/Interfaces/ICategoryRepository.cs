using BanHangThoiTrangMVC.Models.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        List<Category> GetCategoriesOrderByPosition();
    }
}
