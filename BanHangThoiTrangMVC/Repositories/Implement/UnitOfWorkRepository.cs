using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Repositories.Implement
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}