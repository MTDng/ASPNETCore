using System.Collections.Generic;

namespace SportStore.Domain.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> SellectAll();
        T SelectById(object Id);
        void Insert(T obj);

        void Update(T obj);

        void Delete(object Id);
    }
}