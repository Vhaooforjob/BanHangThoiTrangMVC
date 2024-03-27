using BanHangThoiTrangMVC.ExtensionAndHelper;
using BanHangThoiTrangMVC.HelperModels.Paging;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override Task<PagedResult<T>> PagingAsync<F, T>(PagingModel<F> request, Func<List<Product>, List<T>> mapping = null)
    {
        ProductFilterModel filter = null;
        string title = "";
        if(request.Filter != null && request.Filter is ProductFilterModel filterModel)
        {
            filter = filterModel;
            title = filter.Title;
        }

        if (request.Sorts == null || !request.Sorts.Any())
            request.Sorts = new List<SortItems>
            {
                new SortItems
                {
                Column = "Price",
                IsDesc = true,
                }
            };
        
        var queryRes = _dbSet
        .Include(pro => pro.ProductImages)
        .Include(pro => pro.ProductCategory)
        .Where(pro => title == null || string.IsNullOrEmpty(title) || pro.Title.Contains(title))
        .AsQueryable();

        return queryRes
        .OrderProperty(request.Sorts)
        .ToPagingAsync(request.Page, request.Limit, mapping);
    }

    public async Task<Product> GetByIdAndCountAsync(int id)
    {
        Product item = await _dbSet
        .Include(pro => pro.ProductImages)
        .Include(pro => pro.ProductCategory)
        .FirstOrDefaultAsync(pro => pro.Id == id);

        if (item != null)
        {
            _dbSet.Attach(item);
            item.ViewCount = item.ViewCount + 1;
            if (item.ViewCount == 1000)
            {
                item.ViewCount = 0;
            }
            _context.Entry(item).Property(x => x.ViewCount).IsModified = true;
            await _context.SaveChangesAsync();
        }
        return item;
    }
}