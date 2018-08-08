using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoMySqlEF.Models;
using DemoMySQLEF.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace DemoMySQLEF.Models.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext context;
        private IGenericRepository<Course> courseRepo;
        private IGenericRepository<Enrollment> enrollmentRepo;
        private IGenericRepository<Student> studentRepo;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

         public IGenericRepository<Student> StudentRepository
        {
            get
            {
                return studentRepo = studentRepo ?? new GenericRepository<Student>(context);
            }
        }

        public IGenericRepository<Course> CourseRepository
        {
            get
            {
                return courseRepo = courseRepo ?? new GenericRepository<Course>(context);
            }
        }

        public IGenericRepository<Enrollment> EnrollmentRepository
        {
            get
            {
                return enrollmentRepo = enrollmentRepo ?? new GenericRepository<Enrollment>(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}