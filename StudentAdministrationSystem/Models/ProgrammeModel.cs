using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class ProgrammeModel
    {
        public string ProgrammeId { get; set; }
        [Required]
        public string ProgrammeTitle { get; set; }
        [Required]
        public int ProgrammeDuration { get; set; }
        [Required]
        public int ProgrammeModuleNo{ get; set; } 
        public IEnumerable<SelectListItem> Durations { get; set;}
        public string SelectedDurationsId { get; set;}
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<ModuleModel> Module { get; set; }
        public ICollection<StudentModel> Student { get; set; }

        public ProgrammeModel()
        {
            Module = new HashSet<ModuleModel>();
            Student = new HashSet<StudentModel>();
        }
        public ProgrammeModel(Programme programme)
        {
            if (programme == null) return;
            ProgrammeId = programme.ProgrammeId;
            ProgrammeTitle = programme.ProgrammeTitle;
            ProgrammeDuration = programme.ProgrammeDuration;
            ProgrammeModuleNo = programme.ProgrammeModuleNo;
            CreatedDate = programme.CreatedDate;
            Module = new HashSet<ModuleModel>();
            Student = new HashSet<StudentModel>();
        }

        public Programme Create(ProgrammeModel programmeModel)
        {
            return new Programme
            {
                ProgrammeId = programmeModel.ProgrammeId,
                ProgrammeTitle = programmeModel.ProgrammeTitle,
                ProgrammeDuration = programmeModel.ProgrammeDuration,
                ProgrammeModuleNo = programmeModel.ProgrammeModuleNo,
                CreatedDate = DateTime.Now
            };
        }

        public Programme Edit(Programme programmeEntity, ProgrammeModel programmeModel)
        {
            programmeEntity.ProgrammeId = programmeModel.ProgrammeId;
            programmeEntity.ProgrammeTitle = programmeModel.ProgrammeTitle;
            programmeEntity.ProgrammeDuration = programmeModel.ProgrammeDuration;
            programmeEntity.ProgrammeModuleNo = programmeModel.ProgrammeModuleNo;
            programmeEntity.ModifiedDate = programmeModel.ModifiedDate;
            return programmeEntity;
        }

        public string GenerateProgrammeId()
        {
            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");
            return sixDigitNumber;
        }
    }
}