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
        public DbContext DbContext { get; private set; }

        public DbSet<TEntity> DbSet { get; private set; }

        public RepositoryBase() {
            this.DbContext = DbContextFactory.Create(); 
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        #region 同步
        public IEnumerable<TEntity> Get()
        {
            return this.DbContext.Set<TEntity>().AsQueryable();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return this.DbContext.Set<TEntity>().Where(filter).AsQueryable();
        }

        public IEnumerable<TEntity> Get<TOrderkey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderkey>> sortKeySelector, bool isAsc = true)
        {
            if (isAsc)
            {
                return this.DbContext.Set<TEntity>().Where(filter).OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageSize - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                return this.DbContext.Set<TEntity>().Where(filter).OrderByDescending(sortKeySelector)
                   .Skip(pageSize * (pageSize - 1)).Take(pageSize).AsQueryable();
            }
        }

        public bool Add(TEntity instance)
        {
            this.DbContext.Set<TEntity>().Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Added;
            return this.DbContext.SaveChanges() > 0;
        }

        public bool Update(TEntity instance)
        {
            this.DbContext.Set<TEntity>().Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Modified;
            return this.DbContext.SaveChanges() > 0;
        }

        public bool Delete(TEntity instance)
        {
            this.DbContext.Set<TEntity>().Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Deleted;
            return this.DbContext.SaveChanges() > 0;
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return this.DbContext.Set<TEntity>().Where(filter).Count();
        }
        #endregion

        #region 异步
        public async Task<List<TEntity>> GetAsync()
        {
            return await this.DbContext.Set<TEntity>().AsQueryable().ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.DbContext.Set<TEntity>().Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync<TOrderkey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderkey>> sortKeySelector, bool isAsc = true)
        {
            if (isAsc)
            {
                return await this.DbContext.Set<TEntity>().Where(filter).OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageSize - 1)).Take(pageSize).AsQueryable().ToListAsync();
            }
            else
            {
                return await this.DbContext.Set<TEntity>().Where(filter).OrderByDescending(sortKeySelector)
                   .Skip(pageSize * (pageSize - 1)).Take(pageSize).AsQueryable().ToListAsync();
            }
        }

        public async Task<bool> AddAsync(TEntity instance)
        {
            this.DbContext.Set<TEntity>().Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Added;
            return await this.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity instance)
        {
            this.DbContext.Set<TEntity>().Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Modified;
            return await this.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity instance)
        {
            this.DbContext.Set<TEntity>().Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Deleted;
            return await this.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.DbContext.Set<TEntity>().Where(filter).CountAsync();
        }
        #endregion

        public void Dispose() {
            this.DbContext.Dispose();
        }
    }
}
