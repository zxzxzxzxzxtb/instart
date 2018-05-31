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
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> Get<TOrderkey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderkey>> sortKeySelector, bool isAsc = true);
        int Count(Expression<Func<TEntity, bool>> filter);
        bool Add(TEntity instance);
        bool Update(TEntity instance);
        bool Delete(TEntity instance);
    }
}
