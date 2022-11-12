using System.Collections.Generic;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.Service.Interface
{
    public interface IStudentService
    {
        StudentModel[] GetStudents();
        StudentModel GetStudent(string id);
        void AddStudent(StudentModel model);
        void UpdateStudent(StudentModel model);
        void RemoveStudent(string id); 
        StudentModel[] GetStudentByProgramme(string programmeId);
        void AddModuleToStudent(string moduleId, string studentId, string studentModuleId);
        IEnumerable<ModuleModel> GetModuleByStudentId(string studentId);
    }
}