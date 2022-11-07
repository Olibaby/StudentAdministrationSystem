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
            var entities = _gradeRepository.GetGrade(g => g.Assessment, g => g.Student).ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student)
            }).ToArray();
            return models;
        }

        public GradeModel GetGrade(int id)
        {
            var entities = _gradeRepository
                .GetGrade(g => g.Assessment, g => g.Student)
                .Where(g => g.GradeId == id)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student)
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
                .GetGrade(g => g.Assessment, g => g.Student)
                .Where(g => g.AssessmentId == assessmentId)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student)
            }).ToArray();
            return models;
        }

        public GradeModel[] GetGradesByStudent(string studentId)
        {
            var entities = _gradeRepository
                .GetGrade(g => g.Assessment, g => g.Student)
                .Where(g => g.StudentId == studentId)
                .ToList();
            var models = entities.Select(g => new GradeModel(g)
            {
                Assessment = new AssessmentModel(g.Assessment),
                Student = new StudentModel(g.Student)
            }).ToArray();
            return models;
        }
    }
}