using System;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.data.Repository.Interface
{
    public interface IAssessmentRepository
    {
        Assessment GetAssessmentById(int assessmentId);  
        IQueryable<Assessment> GetAssessment(params Expression<Func<Assessment, object>>[] includeProperties); 
        void AddAssessment(Assessment assessment);
        void UpdateAssessment(Assessment assessment);
        void RemoveAssessment(int assessmentId);
    }
}