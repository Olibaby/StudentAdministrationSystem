using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;

namespace StudentAdministrationSystem.data.Repository
{
    public class AssessmentRepositoryImpl: IAssessmentRepository
    {
        private DataEntityContext _context;
        public AssessmentRepositoryImpl() 
        {
            _context = new DataEntityContext();
        }
        public Assessment GetAssessmentById(int assessmentId)
        {
            return _context.Set<Assessment>().Find(assessmentId);
        }

        public IQueryable<Assessment> GetAssessment(params Expression<Func<Assessment, object>>[] includeProperties)
        {
            IQueryable<Assessment> query = _context.Set<Assessment>();
            foreach( var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void AddAssessment(Assessment assessment)
        {
            _context.Set<Assessment>().Add(assessment);
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }

        public void UpdateAssessment(Assessment assessment)
        {
            if(_context.Entry(assessment).State  == EntityState.Detached)
            {
                _context.Set<Assessment>().Attach(assessment);
            }
            _context.Entry(assessment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveAssessment(int assessmentId)
        {
            var item = GetAssessmentById(assessmentId);
            _context.Set<Assessment>().Remove(item);
            _context.SaveChanges();
        }
    }
}