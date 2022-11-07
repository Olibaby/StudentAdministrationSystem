using System.Linq;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Service
{
    public class AssessmentServiceImpl: IAssessmentService
    {
        private IAssessmentRepository _assessmentRepository;
        public AssessmentServiceImpl(IAssessmentRepository assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }
        public AssessmentModel[] GetAssessments()
        {
            var entities = _assessmentRepository.GetAssessment(a => a.Module).ToList();
            var models = entities.Select(a => new AssessmentModel(a)
            {
                Module = new ModuleModel(a.Module)
            }).ToArray();
            return models;
        }

        public AssessmentModel GetAssessment(int id)
        {
            var entities = _assessmentRepository
                .GetAssessment(a => a.Module)
                .Where(a => a.AssessmentId == id)
                .ToList();
            var models = entities.Select(a => new AssessmentModel(a)
            {
                Module = new ModuleModel(a.Module)
            }).FirstOrDefault();
            return models;
        }

        public void AddAssessment(AssessmentModel model)
        {
            var entity = model.Create(model);
            _assessmentRepository.AddAssessment(entity);
        }

        public void UpdateAssessment(AssessmentModel model)
        {
            var programme = _assessmentRepository.GetAssessmentById(model.AssessmentId);
            var entity = model.Edit(programme, model);
            _assessmentRepository.UpdateAssessment(entity);
        }

        public void RemoveAssessment(int id)
        {
            _assessmentRepository.RemoveAssessment(id);
        }

        public AssessmentModel[] GetAssessmentsByModule(string moduleId)
        {
            var entities = _assessmentRepository
                .GetAssessment(a => a.Module)
                .Where(a => a.ModuleId == moduleId)
                .ToList();
            var models = entities.Select(a => new AssessmentModel(a)
            {
                Module = new ModuleModel(a.Module)
            }).ToArray();
            return models;
        }
    }
}