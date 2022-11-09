using System;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.data.Repository.Interface
{
    public interface IStudentRepository
    {
        Student GetStudentById(string studentId);  
        IQueryable<Student> GetStudent(params Expression<Func<Student, object>>[] includeProperties); 
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void RemoveStudent(string studentId);
        void InsertWithData(string moduleId, string studentId);
    }
}