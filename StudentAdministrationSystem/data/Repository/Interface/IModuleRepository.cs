using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.data.Repository.Interface
{
    public interface IModuleRepository
    { 
         IEnumerable<Module> GetModules();
         Module GetModuleById(string moduleId);  
         IQueryable<Module> GetModule(params Expression<Func<Module, object>>[] includeProperties); 
         void AddModule(Module module);
         void UpdateModule(string moduleId,Module module);
         void RemoveModule(string moduleId); 
         // IEnumerable<Module> GetModulesByProgramId(string programmeId);
    }
}