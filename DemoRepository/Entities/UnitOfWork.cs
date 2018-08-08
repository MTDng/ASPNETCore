using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoRepository.Interface;
using DemoRepository.Models;

namespace DemoRepository.Entities
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EntitiesContext entityContext;
        private IGenericRepository<Course> courseRepo;
        private IGenericRepository<Enrollment> enrollmentRepo;

        public UnitOfWork(EntitiesContext entityContext)
        {
            this.entityContext = entityContext;
        }

        public IGenericRepository<Course> CourseRepository
        {
            get
            {
                return courseRepo = courseRepo ?? new GenericRepository<Course>(entityContext);
            }
        }

        public IGenericRepository<Enrollment> EnrollmentRepository
        {
            get
            {
                return enrollmentRepo = enrollmentRepo ?? new GenericRepository<Enrollment>(entityContext);
            }
        }

        public void Save()
        {
            entityContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await entityContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entityContext.Dispose();
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