using BanHangThoiTrangMVC.HelperModels.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductViewModel>> GetListProductAsync(PagingModel<ProductFilterModel> request);
        Task<ProductViewModel> GetByIdAndCountAsync(int id);
    }
}
