using Microsoft.EntityFrameworkCore;
using DemoMySqlEF.Models;

namespace DemoMySqlEF.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Student> Student {get; set;}
        public DbSet<Enrollment> Enrollment {get; set;}
        public DbSet<Course> Course {get; set;}
    }
}