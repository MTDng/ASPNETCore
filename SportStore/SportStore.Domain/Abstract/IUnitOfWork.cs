using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportStore.Domain.Abstract;

namespace SportStore.Abstract
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        void Save();
        Task SaveAsync();
    }
}