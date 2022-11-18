using System;
using System.Collections.Generic;
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
        void InsertStudentWithModule(string moduleId, string studentId, string studentModuleId);
        IEnumerable<Module> GetModuleByStudentIdStatement(string studentId);
        IEnumerable<Grade> GetStudentModulesScore(string studentId);
        // IEnumerable<Module> GetModuleByStudentId(string studentId);
        // IQueryable GetModuleByStudentIdQuery(string studentId);
        // IEnumerable<Student> GetAllStudentsWithModule();
    }
}