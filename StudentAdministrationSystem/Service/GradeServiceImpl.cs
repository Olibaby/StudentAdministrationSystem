using System.Linq;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.Service
{
    public class GradeServiceImpl: IGradeService
    {
        private IGradeRepository _gradeRepository;
        public GradeServiceImpl(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }
        public GradeModel[] GetGrades()
        {
            var entities = _gradeRepository.GetGrade(g => g.Assessment, g => g.Student, g => g.Module).ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student),
                Module = new ModuleModel(g.Module)
            }).ToArray();
            return models;
        }

        public GradeModel GetGrade(int id)
        {
            var entities = _gradeRepository
                .GetGrade(g => g.Assessment, g => g.Student, g=>g.Module)
                .Where(g => g.GradeId == id)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student),
                Module = new ModuleModel(g.Module)
            }).FirstOrDefault();
            return models;
        }

        public void AddGrade(GradeModel model)
        {
            var entity = model.Create(model);
            _gradeRepository.AddGrade(entity);
        }

        public void UpdateGrade(GradeModel model)
        {
            var programme = _gradeRepository.GetGradeById(model.GradeId);
            var entity = model.Edit(programme, model);
            _gradeRepository.UpdateGrade(entity);
        }

        public void RemoveGrade(int id)
        {
            _gradeRepository.RemoveGrade(id);
        }

        public GradeModel[] GetGradesByAssessment(int assessmentId)
        {
            var entities = _gradeRepository
                .GetGrade(g => g.Assessment, g => g.Student, g=>g.Module)
                .Where(g => g.AssessmentId == assessmentId)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student),
                Module = new ModuleModel(g.Module)
            }).ToArray();
            return models;
        }

        public GradeModel[] GetGradesByStudent(string studentId)
        {
            var entities = _gradeRepository
                .GetGrade(g => g.Assessment, g => g.Student, g=>g.Module)
                .Where(g => g.StudentId == studentId)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student),
                Module = new ModuleModel(g.Module)
            }).ToArray();
            return models;
        }
        
        public GradeModel[] GetGradesByStudentModuleAssessment(string studentId, string moduleId, int assessmentId)
        {
            var entities = _gradeRepository
                .GetGrade(g => g.Assessment, g => g.Student, g=>g.Module)
                .Where(g => g.StudentId == studentId && g.ModuleId == moduleId && g.AssessmentId == assessmentId)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student),
                Module = new ModuleModel(g.Module)
            }).ToArray();
            return models;
        }

        public GradeModel[] GetGradesByStudentModule(string studentId, string moduleId)
        {
            var entities = _gradeRepository
                .GetGrade(g => g.Assessment, g => g.Student, g=>g.Module)
                .Where(g => g.StudentId == studentId && g.ModuleId == moduleId)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student),
                Module = new ModuleModel(g.Module)
            }).ToArray();
            return models;
        }
    }
}