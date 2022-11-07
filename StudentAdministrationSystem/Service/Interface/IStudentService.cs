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
    }
}