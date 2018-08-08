using System.Collections.Generic;
using DemoRepository.Interface;
using Microsoft.EntityFrameworkCore;
using DemoRepository.Models;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace DemoRepository.Entities
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        private EntitiesContext context;

        public StudentRepository(EntitiesContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await context.Student.ToListAsync();
        }

        public async Task<Student> GetStudentByIDAsync(int id)
        {
            return await context.Student.FindAsync(id);
        }

        public async Task InsertStudentAsync(Student student)
        {
            await context.Student.AddAsync(student);
        }

        public async Task DeleteStudentAsync(int studentID)
        {
            Student student = await context.Student.FindAsync(studentID);
            context.Student.Remove(student);
        }

        public void UpdateStudent(Student student)
        {
            context.Entry(student).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
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