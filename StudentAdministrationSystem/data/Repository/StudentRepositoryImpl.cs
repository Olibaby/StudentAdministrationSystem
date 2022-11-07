using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;

namespace StudentAdministrationSystem.data.Repository
{
    public class StudentRepositoryImpl: IStudentRepository
    {
        private DataEntityContext _context;
        public StudentRepositoryImpl() 
        {
            _context = new DataEntityContext();
        }
        public Student GetStudentById(string studentId)
        {
            return _context.Set<Student>().Find(studentId);
        }

        public IQueryable<Student> GetStudent(params Expression<Func<Student, object>>[] includeProperties)
        {
            IQueryable<Student> query = _context.Set<Student>();
            foreach( var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void AddStudent(Student student)
        {
            _context.Set<Student>().Add(student);
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

        public void UpdateStudent(Student student)
        {
            if(_context.Entry(student).State  == EntityState.Detached)
            {
                _context.Set<Student>().Attach(student);
            }
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveStudent(string studentId)
        {
            var item = GetStudentById(studentId);
            _context.Set<Student>().Remove(item);
            _context.SaveChanges();
        }
    }
}