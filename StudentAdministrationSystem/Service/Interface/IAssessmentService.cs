using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.Service.Interface
{
    public interface IAssessmentService
    {
        AssessmentModel[] GetAssessments();
        AssessmentModel GetAssessment(int id);
        void AddAssessment(AssessmentModel model);
        void UpdateAssessment(AssessmentModel model);
        void RemoveAssessment(int id); 
        AssessmentModel[] GetAssessmentsByModule(string moduleId);
    } 
}