using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Strategy
{
    public interface IProductSortingStrategy
    {
        IQueryable<ProductViewModel> SortProducts(IQueryable<ProductViewModel> products);
    }
}
