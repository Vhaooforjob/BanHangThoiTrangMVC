using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Repositories.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
