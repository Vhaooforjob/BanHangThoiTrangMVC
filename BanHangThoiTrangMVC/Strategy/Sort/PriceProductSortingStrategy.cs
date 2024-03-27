using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangThoiTrangMVC.Strategy.Sort
{
    public class PriceProductSortingStrategy : IProductSortingStrategy
    {
        public PriceProductSortingStrategy() { }
        public IQueryable<ProductViewModel> SortProducts(IQueryable<ProductViewModel> products)
        {
            return products.OrderBy(p => p.Price);
        }
    }
}