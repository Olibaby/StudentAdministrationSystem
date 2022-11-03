using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class ProgrammeModel
    {
        public string ProgrammeId { get; set; }
        public string ProgrammeTitle { get; set; }
        public string ProgrammeDuration { get; set; }
        public IEnumerable<SelectListItem> Durations { get; set;}
        public int SelectedDurationsId { get; set;}
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ProgrammeModel()
        {
            
        }
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
            string selectedDuration = "";
            Console.WriteLine("id is " + SelectedDurationsId );
            foreach (var item in Durations)
            {
                Console.WriteLine("id here is " + item.Value );
                Console.WriteLine("parse id here is " + int.Parse(item.Value));
                if (SelectedDurationsId == int.Parse(item.Value))
                {
                    selectedDuration = item.Text;
                }
            }
            Console.WriteLine("selected is" + selectedDuration );
            return new Programme
            {
                ProgrammeId = generateProgrammeId(),
                ProgrammeTitle = programmeModel.ProgrammeTitle,
                ProgrammeDuration = selectedDuration,
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

        private string generateProgrammeId()
        {
            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");
            Console.WriteLine("number is" + sixDigitNumber);
            return sixDigitNumber;
        }
    }
}