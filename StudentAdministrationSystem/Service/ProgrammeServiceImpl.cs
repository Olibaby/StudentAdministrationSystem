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
            model.ProgrammeId = model.GenerateProgrammeId();
            model.ProgrammeModuleNo = model.ProgrammeDuration * 6;
            var entity = model.Create(model);
            _programmeRepository.AddProgramme(entity);
        }

        public void UpdateProgramme(ProgrammeModel model)
        {
            model.ProgrammeModuleNo = model.ProgrammeDuration * 6;
            model.ModifiedDate = DateTime.Now;
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