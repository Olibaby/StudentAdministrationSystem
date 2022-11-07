using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;

namespace StudentAdministrationSystem.data.Repository
{
    public class ModuleRepositoryImpl: IModuleRepository
    {
        private DataEntityContext _context;
        public ModuleRepositoryImpl() 
        {
            _context = new DataEntityContext();
        }
        public IEnumerable<Module> GetModules()
        {
            return _context.Set<Module>().ToList();
        }
        public Module GetModuleById(string moduleId)
        {
            return _context.Set<Module>().Find(moduleId);
        }
        public IQueryable<Module> GetModule(params Expression<Func<Module, object>>[] includeProperties)
        {
            IQueryable<Module> query = _context.Set<Module>();
            foreach( var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        public void AddModule(Module module)
        {
            _context.Set<Module>().Add(module);
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
        public void UpdateModule(string moduleId, Module module)
        {
            if(_context.Entry(module).State  == EntityState.Detached)
            {
                _context.Set<Module>().Attach(module);
            }
            _context.Entry(module).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveModule(string moduleId)
        {
            var item = GetModuleById(moduleId);
            _context.Set<Module>().Remove(item);
            _context.SaveChanges();
        }

        // public IEnumerable<Module> GetModulesByProgramId(string programmeId)
        // {
        //     return _context.Set<Module>().Where(m=> m.ProgrammeId == programmeId);
        // }
    }
}