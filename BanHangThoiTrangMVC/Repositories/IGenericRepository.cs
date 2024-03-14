using BanHangThoiTrangMVC.EntityTypeConfigurations.Interfaces;
using BanHangThoiTrangMVC.HelperModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault();
        Task<TEntity> FindAsync(params object[] keyValues);
        TEntity Find(params object[] keyValues);
        void RemoveRange(params TEntity[] entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void UpdateRange(params TEntity[] entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        IEnumerable<TEntity> AddRange(params TEntity[] entities);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity Add(TEntity entity);

        Task<PagedResult<TEntity>> PagingAsync<T>(PagingModel<T> request);

        Task<PagedResult<TEntity>> PagingAsync<T>(PagingModel<T> request, Expression<Func<TEntity, bool>> predicate);

        Task<PagedResult<T>> PagingAsync<F, T>(PagingModel<F> request, Func<List<TEntity>, List<T>> mapping);
    }
}
