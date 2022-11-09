using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
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

        public void InsertWithData(string moduleId, string studentId)
        {
            //add an existing module to a student that had one module already
            var student = _context.Students
                .Include(p => p.Modules)
                .Single(p => p.StudentId == studentId);
            var existingModule = _context.Modules         
                .Single(m => m.ModuleId == moduleId);
            student.Modules.Add(existingModule);
            _context.SaveChanges();
            
           
           //get all students with their modules
           // var students = _context.Students.Include(b => b.Modules).ToList();
           //  //Get all the students with the moduleId
           //  var tester = _context.Modules.Select(m => m.ModuleId).ToList();
           //  var testino = _context.Students.Where(s => s.ProgrammeId == moduleId).ToList();
           //  //remove 
           // var student2 = _context.Students
           //     .Include(p => p.Modules)
           //     .First();
           //
           // var moduleToRemove = student2.Modules
           //     .Single(x => x.ModuleId == moduleId);
           // student2.Modules.Remove(moduleToRemove);
           // _context.SaveChanges();
           //
           // //add two existing module to new student
           // var existingTag1 = _context.Modules.Single(m => m.ModuleId == moduleId);
           // var existingTag2 = _context.Modules.Single(m => m.ModuleId == moduleId);
           // var newStudent= new Student()
           // {
           //     StudentName = "Olivia",
           //     //... other property settings left out
           //     //Set your Tags property to an empty collection
           //     Modules = new List<Module>()
           // };
           // newStudent.Modules.Add(existingTag1);
           // newStudent.Modules.Add(existingTag2);
           // _context.Set<Student>().Add(newStudent);
           // _context.SaveChanges();
        }
    }
}