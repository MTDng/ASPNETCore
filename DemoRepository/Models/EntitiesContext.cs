using Microsoft.EntityFrameworkCore;
using DemoRepository.Entities;

namespace DemoRepository.Models
{
    public class EntitiesContext: DbContext
    {
        public EntitiesContext(DbContextOptions<EntitiesContext> options): base(options)
        {

        }

        public DbSet<Student> Student {get; set;}
        public DbSet<Enrollment> Enrollment {get; set;}
        public DbSet<Course> Course {get; set;}
    }
}