using System;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class ProgrammeModel
    {
        public string ProgrammeId { get; set; }
        public string ProgrammeTitle { get; set; }
        public string ProgrammeDuration { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ProgrammeModel(Programme programme)
        {
            if (programme == null) return;
            ProgrammeId = programme.ProgrammeId;
            ProgrammeTitle = programme.ProgrammeTitle;
            ProgrammeDuration = programme.ProgrammeDuration;
            CreatedDate = DateTime.Now;
        }

        public Programme Create(ProgrammeModel programmeModel)
        {
            return new Programme
            {
                ProgrammeId = programmeModel.ProgrammeId,
                ProgrammeTitle = programmeModel.ProgrammeTitle,
                CreatedDate = DateTime.Now
            };
        }

        public Programme Edit(Programme programmeEntity, ProgrammeModel programmeModel)
        {
            programmeEntity.ProgrammeId = programmeModel.ProgrammeId;
            programmeEntity.ProgrammeTitle = programmeModel.ProgrammeTitle;
            programmeEntity.ProgrammeDuration = programmeModel.ProgrammeDuration;
            programmeEntity.ModifiedDate = programmeModel.ModifiedDate;
            return programmeEntity;
        }
    }
}