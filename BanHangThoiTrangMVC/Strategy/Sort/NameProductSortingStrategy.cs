using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangThoiTrangMVC.Strategy.Sort
{
    public class NameProductSortingStrategy : IProductSortingStrategy
    {
        public NameProductSortingStrategy()
        {

        }
        public IQueryable<ProductViewModel> SortProducts(IQueryable<ProductViewModel> products)
        {
            return products.OrderBy(p => p.Title);
        }
    }
}