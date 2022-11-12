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

        // public IEnumerable<Student> GetAllStudentsWithModule()
        // {
        //     //get all students with their modules
        //     var students = _context.Students.Include(b => b.Modules).ToList();
        //     return students;
        // }
        
        // public IQueryable GetModuleByStudentIdQuery(string studentId)
        // {
        //     var query = _context.Students.Include(p => p.Modules).Join(_context.ModuleStudents,
        //         student => student.StudentId,
        //         moduleStudent => moduleStudent.Student_StudentId,
        //         (student, moduleStudent) => new { student, moduleStudent }).Join(
        //         _context.Modules.Include(m => m.Programme),
        //         combinedEntry => combinedEntry.moduleStudent.Module_ModuleId,
        //         module => module.ModuleId, (combinedEntry, module) => new
        //         {
        //             // smId = combinedEntry.moduleStudent.Student_StudentId,
        //             // msId = combinedEntry.moduleStudent.Module_ModuleId,
        //             sId = combinedEntry.student.StudentId,
        //             Module = module.ModuleTitle
        //         }).Where(fullEntry => fullEntry.sId == studentId);
        //     return query;
        //}
        
        // public IEnumerable<Module> GetModuleByStudentId(string studentId)
        // {
        //    var query =
        //        $"Select m.* from students as s " +
        //        $"left join modulestudents as ms on s.StudentId = ms.Student_StudentId " +
        //        $"left join modules as m  on m.ModuleId = ms.Module_ModuleId where s.StudentId = '{studentId}'";
        //    return _context.Database.SqlQuery<IEnumerable<Module>>(query, new SqlParameter("@studentId", studentId)).FirstOrDefault();
        // }
    }
}

//  //remove 
// var student2 = _context.Students
//     .Include(p => p.Modules)
//     .First();
// var moduleToRemove = student2.Modules
//     .Single(x => x.ModuleId == moduleId);
// student2.Modules.Remove(moduleToRemove);
// _context.SaveChanges();
           
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
