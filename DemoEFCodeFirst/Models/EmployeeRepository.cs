using System.Linq;
using System.Threading.Tasks;
using DemoEFCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCodeFirst.Models
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DemoDbContext dbContext)
        : base(dbContext)
        {
            
        }

        public async Task<Employee> GetSortedEmployeeAsync()
        {
            return await GetAll()
                .OrderByDescending(c => c.Name)
                .FirstOrDefaultAsync();
        }
    }
}