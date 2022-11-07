using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.Service.Interface
{
    public interface IModuleService
    {
        ModuleModel[] GetModules();
        ModuleModel GetModule(string id);
        void AddModule(ModuleModel model);
        void UpdateModule(ModuleModel model);
        void RemoveModule(string id); 
        ModuleModel[] GetModulesByProgramme(string programmeId);
    }
}