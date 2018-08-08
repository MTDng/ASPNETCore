using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SportStore.Domain.Abstract;

namespace SportStore.Domain.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<T> dbSet;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Db.Set<T>();
        }

        public void Delete(object Id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new System.NotImplementedException();
        }

        public T SelectById(object Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> SellectAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}