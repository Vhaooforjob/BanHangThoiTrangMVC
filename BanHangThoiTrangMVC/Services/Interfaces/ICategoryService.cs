using BanHangThoiTrangMVC.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryViewModel> GetCategoriesList();
    }
}
