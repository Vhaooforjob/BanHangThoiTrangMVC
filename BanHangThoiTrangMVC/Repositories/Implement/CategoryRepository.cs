using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Repositories.Implement
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Category> GetCategoriesOrderByPosition()
        {
            return _dbSet.OrderBy(c => c.Position).ToList();
        }
    }
}