using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.Service.Interface
{
    public interface IProgrammeService
    {
        ProgrammeModel[] GetProgrammes();
        ProgrammeModel GetProgramme(string id);
        void AddProgramme(ProgrammeModel model);
        void UpdateProgramme(ProgrammeModel model);
        void RemoveProgramme(string id);
    }
}