using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public InstartDbContext DbContext { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }
        public RepositoryBase(InstartDbContext context) {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get() {
            return this.DbSet.AsQueryable();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter) {
            return this.DbSet.Where(filter).AsQueryable();
        }

        public IEnumerable<TEntity> Get<TOrderkey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderkey>> sortKeySelector, bool isAsc = true) {
            if (isAsc) {
                return this.DbSet.Where(filter).OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageSize - 1)).Take(pageSize).AsQueryable();
            }
            else {
                return this.DbSet.Where(filter).OrderByDescending(sortKeySelector)
                   .Skip(pageSize * (pageSize - 1)).Take(pageSize).AsQueryable();
            }
        }

        public bool Add(TEntity instance) {
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Added;
            return this.DbContext.SaveChanges() > 0;
        }

        public bool Update(TEntity instance) {
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Modified;
            return this.DbContext.SaveChanges() > 0;
        }

        public bool Delete(TEntity instance) {
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Deleted;
            return this.DbContext.SaveChanges() > 0;
        }

        public int Count(Expression<Func<TEntity, bool>> filter) {
            return this.DbSet.Where(filter).Count();
        }

        public void Dispose() {
            this.DbContext.Dispose();
        }
    }
}
