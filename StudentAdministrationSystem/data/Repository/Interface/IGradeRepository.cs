using System;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.data.Repository.Interface
{
    public interface IGradeRepository
    {
        Grade GetGradeById(int gradeId);  
        IQueryable<Grade> GetGrade(params Expression<Func<Grade, object>>[] includeProperties); 
        void AddGrade(Grade grade);
        void UpdateGrade(Grade grade);
        void RemoveGrade(int gradeId); 
    }
}