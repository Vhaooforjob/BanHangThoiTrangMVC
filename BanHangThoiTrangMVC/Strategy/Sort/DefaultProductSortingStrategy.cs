using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangThoiTrangMVC.Strategy.Sort
{
    public class DefaultProductSortingStrategy : IProductSortingStrategy
    {
        public DefaultProductSortingStrategy()
        {

        }
        public IQueryable<ProductViewModel> SortProducts(IQueryable<ProductViewModel> products)
        {
            return products.OrderBy(p => p.Id);
        }
    }
}