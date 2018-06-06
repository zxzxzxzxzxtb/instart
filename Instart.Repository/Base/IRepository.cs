using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, TEntity>> selector);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> selector);
        IEnumerable<TEntity> Get<TOrderkey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> selector, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderkey>> sortKeySelector, bool isAsc = true);
        int Count(Expression<Func<TEntity, bool>> filter);
        bool Add(TEntity instance);
        bool Update(TEntity instance);
        bool Delete(TEntity instance);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, TEntity>> selector);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> selector);
        Task<List<TEntity>> GetAsync<TOrderkey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> selector, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderkey>> sortKeySelector, bool isAsc = true);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AddAsync(TEntity instance);
        Task<bool> UpdateAsync(TEntity instance);
        Task<bool> DeleteAsync(TEntity instance);
    }
}
