using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportStore.Abstract;
using SportStore.Domain.Abstract;
using SportStore.Domain.Concrete;

namespace SportStore.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext entities = null;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(EntitiesContext entities)
        {
            this.entities = entities;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if(repositories.Keys.Contains(typeof(T)))
            {
                return repositories[typeof(T)] as IGenericRepository<T>;
            }
            IGenericRepository<T> repo = new GenericRepository<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void Save()
        {
            entities.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await entities.SaveChangesAsync();
        }
    }
}