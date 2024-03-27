using BanHangThoiTrangMVC.HelperModels.Paging;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Services.Interfaces;
using BanHangThoiTrangMVC.Strategy;
using BanHangThoiTrangMVC.Strategy.Sort;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace BanHangThoiTrangMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductSortingStrategy _defaultSortingStrategy;
        private readonly IProductSortingStrategy _priceSortingStrategy;
        private readonly IProductSortingStrategy _nameSortingStrategy;
        private ApplicationDbContext db = new ApplicationDbContext();

        //public ProductsController() { }
        public ProductsController(IProductService productService,IProductSortingStrategy defaultSortingStrategy,
        IProductSortingStrategy priceSortingStrategy,IProductSortingStrategy nameSortingStrategy)
        {
            _productService = productService;
            _defaultSortingStrategy = defaultSortingStrategy;
            _priceSortingStrategy = priceSortingStrategy;
            _nameSortingStrategy = nameSortingStrategy;
        }

        // GET: Products
        public async Task<ActionResult> Index(string Searchtext, int? id, int? page, int? limit, string sortBy)
        {

            PagingModel<ProductFilterModel> request = new PagingModel<ProductFilterModel>
            {
                Limit = limit ?? 8,
                Page = page ?? 0,
                Filter = new ProductFilterModel
                {
                    Title = Searchtext,
                }
            };
            IProductSortingStrategy sortingStrategy;
            switch (sortBy)
            {
                case "price":
                    sortingStrategy = new PriceProductSortingStrategy();
                    break;
                case "name":
                    sortingStrategy = _nameSortingStrategy;
                    break;
                default:
                    sortingStrategy = _defaultSortingStrategy;
                    break;
            }
            var products = await _productService.GetListProductAsync(request);

            var sortedProducts = sortingStrategy.SortProducts(products.Items.AsQueryable());
            
            var pagedResult = new PagedResult<ProductViewModel>
            {
                Items = sortedProducts.ToList(),
                Page = request.Page,
                Limit = request.Limit,
                Total = sortedProducts.Count()
            };

            return View(pagedResult);
            //return View(await this._productService.GetListProductAsync(request));
        }

        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.Where(x => x.IsActive).Take(12).ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSale()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public async Task<ActionResult> Detail(string alias, int id)
        {
            ProductViewModel item = await _productService.GetByIdAndCountAsync(id);
            return View(item);
        }

    }
}