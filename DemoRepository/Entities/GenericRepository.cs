using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoRepository.Interface;
using DemoRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoRepository.Entities
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal EntitiesContext entityContext;
        public GenericRepository(EntitiesContext entityContext)
        {
            this.entityContext = entityContext;

        }

        public IQueryable<T> GetAll()
        {
            return entityContext.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {

            return await entityContext.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return entityContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await entityContext.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {

            entityContext.Set<T>().Add(t);
            entityContext.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            entityContext.Set<T>().Add(t);
            await entityContext.SaveChangesAsync();
            return t;

        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return entityContext.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await entityContext.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return entityContext.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await entityContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            entityContext.Set<T>().Remove(entity);
            entityContext.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            entityContext.Set<T>().Remove(entity);
            return await entityContext.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = entityContext.Set<T>().Find(key);
            if (exist != null)
            {
                entityContext.Entry(exist).CurrentValues.SetValues(t);
                entityContext.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await entityContext.Set<T>().FindAsync(key);
            if (exist != null)
            {
                entityContext.Entry(exist).CurrentValues.SetValues(t);
                await entityContext.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return entityContext.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await entityContext.Set<T>().CountAsync();
        }

        public virtual void Save()
        {

            entityContext.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await entityContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = entityContext.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await entityContext.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entityContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}