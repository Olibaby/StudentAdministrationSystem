using System.Linq;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Service
{
    public class StudentServiceImpl: IStudentService
    {
        private IStudentRepository _studentRepository;
        public StudentServiceImpl(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public StudentModel[] GetStudents()
        {
            var entities = _studentRepository.GetStudent(s => s.Programme ).ToList();
            var models = entities.Select(s => new StudentModel(s)
            {
                Programme = new ProgrammeModel(s.Programme)
            }).ToArray();
            return models;
        }

        public StudentModel GetStudent(string id)
        {
            var entities = _studentRepository
                .GetStudent(s => s.Programme)
                .Where(s => s.StudentId == id)
                .ToList();
            var models = entities.Select(s => new StudentModel(s)
            {
                Programme = new ProgrammeModel(s.Programme)
            }).FirstOrDefault();
            return models;
        }

        public void AddStudent(StudentModel model)
        {
            var entity = model.Create(model);
            _studentRepository.AddStudent(entity);
        }

        public void UpdateStudent(StudentModel model)
        {
            var programme = _studentRepository.GetStudentById(model.StudentId);
            var entity = model.Edit(programme, model);
            _studentRepository.UpdateStudent(entity);
        }

        public void RemoveStudent(string id)
        {
            _studentRepository.RemoveStudent(id);
        }

        public StudentModel[] GetStudentByProgramme(string programmeId)
        {
            var entities = _studentRepository
                .GetStudent(s => s.Programme)
                .Where(s => s.ProgrammeId == programmeId)
                .ToList();
            var models = entities.Select(s => new StudentModel(s)
            {
                Programme = new ProgrammeModel(s.Programme)
            }).ToArray();
            return models;
        }

        public void AddModuleToStudent(string moduleId, string studentId)
        {
            _studentRepository.InsertWithData(moduleId, studentId);
        }
    }
}