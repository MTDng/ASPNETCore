using System;
using System.Threading.Tasks;
using DemoMySqlEF.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMySQLEF.Models.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<Student> StudentRepository { get; }
        IGenericRepository<Course> CourseRepository { get; }
        IGenericRepository<Enrollment> EnrollmentRepository { get; }

        void Save();
        Task SaveAsync();
    }
}