using System.Threading.Tasks;
using DemoEFCodeFirst.Models;

namespace DemoEFCodeFirst.Models
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<Employee> GetSortedEmployeeAsync();
    }
}