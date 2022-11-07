using System;
using System.Linq;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Service
{
    public class ProgrammeServiceImpl: IProgrammeService
    {
        private IProgrammeRepository _programmeRepository;

        public ProgrammeServiceImpl(IProgrammeRepository programmeRepository)
        {
            _programmeRepository = programmeRepository;
        }
        
        public ProgrammeModel[] GetProgrammes()
        {
            var entities = _programmeRepository.GetProgrammes();
            return entities.Select(p => new ProgrammeModel(p)).ToArray();
        }

        public ProgrammeModel GetProgramme(string id)
        {
            var entity =_programmeRepository.GetProgrammeById(id);
            return new ProgrammeModel(entity);
        }

        public void AddProgramme(ProgrammeModel model)
        {
            var entity = model.Create(model);
            _programmeRepository.AddProgramme(entity);
        }

        public void UpdateProgramme(string id, ProgrammeModel model)
        {
            var programme = _programmeRepository.GetProgrammeById(model.ProgrammeId);
            var entity = model.Edit(programme, model);
            _programmeRepository.UpdateProgramme(model.ProgrammeId,entity);
        }

        public void RemoveProgramme(string id)
        {
            _programmeRepository.RemoveProgramme(id);
        }
    }
}