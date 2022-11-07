using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;

namespace StudentAdministrationSystem.data.Repository
{
    public class GradeRepositoryImpl: IGradeRepository
    {
        private DataEntityContext _context;
        public GradeRepositoryImpl() 
        {
            _context = new DataEntityContext();
        }
        public Grade GetGradeById(int gradeId)
        {
            return _context.Set<Grade>().Find(gradeId);
        }

        public IQueryable<Grade> GetGrade(params Expression<Func<Grade, object>>[] includeProperties)
        {
            IQueryable<Grade> query = _context.Set<Grade>();
            foreach( var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void AddGrade(Grade grade)
        {
            _context.Set<Grade>().Add(grade);
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

        public void UpdateGrade(Grade grade)
        {
            if(_context.Entry(grade).State  == EntityState.Detached)
            {
                _context.Set<Grade>().Attach(grade);
            }
            _context.Entry(grade).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveGrade(int gradeId)
        {
            var item = GetGradeById(gradeId);
            _context.Set<Grade>().Remove(item);
            _context.SaveChanges();
        }
    }
}