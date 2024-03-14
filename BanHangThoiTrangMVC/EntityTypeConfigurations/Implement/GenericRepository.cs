using BanHangThoiTrangMVC.EntityTypeConfigurations.Interfaces;
using BanHangThoiTrangMVC.ExtensionAndHelper;
using BanHangThoiTrangMVC.HelperModels.Paging;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.Where(predicate).ToListAsync();
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return this._dbSet.Where(predicate);
    }


    public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.FirstOrDefaultAsync(predicate);
    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return this._dbSet.FirstOrDefault(predicate);
    }
    public virtual TEntity FirstOrDefault()
            => _dbSet.FirstOrDefault();


    public virtual Task<TEntity> FindAsync(params object[] keyValues)
            => _dbSet.FindAsync(keyValues);
    public virtual TEntity Find(params object[] keyValues)
            => _dbSet.Find(keyValues);


    public virtual void RemoveRange(params TEntity[] entities)
            => entities.ToList().ForEach(entity => Remove(entity));
    public virtual void RemoveRange(IEnumerable<TEntity> entities)
            => entities.ToList().ForEach(entity => Remove(entity));
    public virtual void Remove(TEntity entity)
            => _dbSet.Remove(entity);


    public virtual void UpdateRange(params TEntity[] entities)
            => entities.ToList().ForEach(entity => Update(entity));
    public virtual void UpdateRange(IEnumerable<TEntity> entities)
            => entities.ToList().ForEach(entity => Update(entity));
    public virtual void Update(TEntity entity)
            => _dbSet.AddOrUpdate(entity);


    public virtual IEnumerable<TEntity> AddRange(params TEntity[] entities)
           => _dbSet.AddRange(entities);
    public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
           => _dbSet.AddRange(entities);
    public virtual TEntity Add(TEntity entity)
            => _dbSet.Add(entity);


    public virtual Task<PagedResult<TEntity>> PagingAsync<T>(PagingModel<T> request)
            => _dbSet.OrderProperty(request.Sorts).ToPagingAsync<TEntity, TEntity>(request.Page, request.Limit);
    public virtual Task<PagedResult<TEntity>> PagingAsync<T>(PagingModel<T> request, Expression<Func<TEntity, bool>> predicate)
            => _dbSet.Where(predicate).OrderProperty(request.Sorts).ToPagingAsync<TEntity, TEntity>(request.Page, request.Limit);
    public virtual Task<PagedResult<T>> PagingAsync<F, T>(PagingModel<F> request, Func<List<TEntity>, List<T>> mapping)
            => _dbSet.OrderProperty(request.Sorts).ToPagingAsync(request.Page, request.Limit, mapping);


    //public virtual Task<PagedResult<TEntity>> PagingAsync<T>(PagingModel<T> request)
    //{
    //    return this._dbSet
    //       .OrderProperty(request.Sorts)
    //       .ToPagingAsync<TEntity, TEntity>(request.Page, request.Limit);
    //}


}