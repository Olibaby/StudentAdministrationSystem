using System.Linq;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Service
{
    public class ModuleServiceImpl: IModuleService
    {
        private IModuleRepository _moduleRepository;
        public ModuleServiceImpl(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }
        public ModuleModel[] GetModules()
        {
            // var entities = _moduleRepository.GetModules();
            // return entities.Select(m => new ModuleModel(m)).ToArray();
            var entities = _moduleRepository.GetModule(m => m.Programme ).ToList();
            var models = entities.Select(m => new ModuleModel(m)
            {
                Programme = new ProgrammeModel(m.Programme)
            }).ToArray();
            return models;
        }

        public ModuleModel GetModule(string id)
        {
            // var entity =_moduleRepository.GetModuleById(id);
            // return new ModuleModel(entity);
            var entities = _moduleRepository
                .GetModule(m => m.Programme)
                .Where(m => m.ModuleId == id)
                .ToList();
            var models = entities.Select(c => new ModuleModel(c)
            {
                Programme = new ProgrammeModel(c.Programme)
            }).FirstOrDefault();
            return models;
        }

        public void AddModule(ModuleModel model)
        {
            var entity = model.Create(model);
            _moduleRepository.AddModule(entity);
        }

        public void UpdateModule(ModuleModel model)
        {
            var programme = _moduleRepository.GetModuleById(model.ProgrammeId);
            var entity = model.Edit(programme, model);
            _moduleRepository.UpdateModule(model.ProgrammeId,entity);
        }

        public void RemoveModule(string id)
        {
            _moduleRepository.RemoveModule(id);
        }

        public ModuleModel[] GetModulesByProgramme(string programmeId)
        {
            var entities = _moduleRepository
                .GetModule(m => m.Programme)
                .Where(m => m.ProgrammeId == programmeId)
                .ToList();
            var models = entities.Select(c => new ModuleModel(c)
            {
                Programme = new ProgrammeModel(c.Programme)
            }).ToArray();
            return models;
        }
    }
}