using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoRepository.Entities;

namespace DemoRepository.Interface
{
    public interface IStudentRepository : IDisposable
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIDAsync(int studentId);
        Task InsertStudentAsync(Student student);
        Task DeleteStudentAsync(int studentID);
        void UpdateStudent(Student student);
        Task SaveAsync();
    }
}