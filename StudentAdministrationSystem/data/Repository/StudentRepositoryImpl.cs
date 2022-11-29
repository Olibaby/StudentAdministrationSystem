using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;

namespace StudentAdministrationSystem.data.Repository
{
    public class StudentRepositoryImpl: IStudentRepository
    {
        private DataEntityContext _context;
        private IModuleRepository _moduleRepository;
        public StudentRepositoryImpl(IModuleRepository moduleRepository) 
        {
            _context = new DataEntityContext();
            _moduleRepository = moduleRepository;
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

        public void InsertStudentWithModule(string moduleId, string studentId, string studentModuleId)
        {
            _context.StudentModules.Add(new StudentModule
            {
                StudentModuleId = studentModuleId,
                ModuleId = moduleId,
                StudentId = studentId
            });
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
            // add an existing module to a student that had one module already
             // var student = _context.Students
             //     .Include(p => p.Modules)
             //     .Single(p => p.StudentId == studentId);
             // var existingModule = _context.Modules         
             //     .Single(m => m.ModuleId == moduleId);
             // student.Modules.Add(existingModule);
             // _context.SaveChanges();
        }

        public IEnumerable<Module> GetModuleByStudentIdStatement(string studentId)
        {
            var mods = new List<Module>();
            var leftOuterJoin = from s in _context.Students  
                join ms in _context.StudentModules on s.StudentId equals ms.StudentId
                join m in _context.Modules on ms.ModuleId equals m.ModuleId 
                where s.StudentId == studentId
                select new
                {  
                    ModuleId = m.ModuleId,
                    ModuleName = m.ModuleTitle, 
                    ModuleType = m.ModuleType,
                    ProgrammeId = m.ProgrammeId
                  
                };
            foreach (var data in leftOuterJoin)  
            {  
                Console.WriteLine("modules are " + data.ModuleName);  
                var mod = new Module
                {
                    ModuleId = data.ModuleId,
                    ModuleTitle = data.ModuleName,
                    ModuleType = data.ModuleType,
                    ProgrammeId = data.ProgrammeId,
                    Programme = new Programme()
                };
                mods.Add(mod);
            }
            Console.WriteLine("mod count is " + mods.Count());
            return mods;
        }

        public IEnumerable<Grade> GetStudentModulesScore(string studentId)
        {
            var moduleSums = new List<Grade>();
            var sumQuery = from g in _context.Grades
                where g.StudentId == studentId
                group g by g.ModuleId
                into m
                select new
                {
                    ModuleSum = m.Sum(c => c.Mark)
                };
             //lambda
            var query = _context.Grades
                .Where(c=> c.StudentId == studentId)
                .GroupBy(g => new { ID = g.ModuleId })
                .Select(m => new
                {
                    ModuleSum = m.Sum(s => s.Mark),
                    ModuleId = m.Key.ID,
                    ModuleTitle = m.FirstOrDefault(f=>f.ModuleId == m.Key.ID).Module
                });

            foreach (var sums in query)
            {
                var grade = new Grade
                {
                    Mark = sums.ModuleSum,
                    ModuleId = sums.ModuleId,
                    Module = sums.ModuleTitle
                };
                moduleSums.Add(grade);
            }

            return moduleSums;
        }

    }
}


