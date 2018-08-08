using System;
using System.Threading.Tasks;
using DemoRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoRepository.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<Course> CourseRepository { get; }
        IGenericRepository<Enrollment> EnrollmentRepository { get; }

        void Save();
        Task SaveAsync();
    }

    public interface IUnitOfWork<TContext> where TContext: DbContext
    {
        TContext DbContext { get; }

        void Save();
        Task SaveAsync();
    }
}