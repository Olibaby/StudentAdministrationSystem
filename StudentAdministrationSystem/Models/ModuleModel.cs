using System;
using System.Collections.Generic;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class ModuleModel
    {
        public string ModuleId { get; set; }
        public string ModuleTitle { get; set; }
        public string ModuleType { get; set; }
        public string ProgrammeId { get; set; }
        public bool isSelected { get; set; }
        public ProgrammeModel Programme { get; set; }
        public ICollection<AssessmentModel> Assessment { get; set; }
        public ICollection<GradeModel> Grade { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        

        public ModuleModel()
        {
            Programme = new ProgrammeModel();
            Assessment = new HashSet<AssessmentModel>();
            Grade = new HashSet<GradeModel>();
        }

        public ModuleModel(Module module)
        {
            if (module == null) return;
            ModuleId = module.ModuleId;
            ModuleTitle = module.ModuleTitle;
            ModuleType = module.ModuleType;
            ProgrammeId = module.ProgrammeId;
            Programme = new ProgrammeModel();
            Assessment = new HashSet<AssessmentModel>();
            Grade = new HashSet<GradeModel>();
            CreatedDate = module.CreatedDate;
        }

        public Module Create(ModuleModel moduleModel)
        {
            return new Module
            {
                ModuleId = GenerateModuleId(),
                ModuleTitle = moduleModel.ModuleTitle,
                ModuleType = moduleModel.ModuleType,
                ProgrammeId = moduleModel.ProgrammeId,
                CreatedDate = DateTime.Now
            };
        }

        public Module Edit(Module moduleEntity, ModuleModel moduleModel)
        {
            moduleEntity.ModuleId = moduleModel.ModuleId;
            moduleEntity.ModuleTitle = moduleModel.ModuleTitle;
            moduleEntity.ModuleType = moduleModel.ModuleType;
            moduleEntity.ModifiedDate = moduleModel.ModifiedDate;
            moduleEntity.ProgrammeId = moduleModel.ProgrammeId;
            return moduleEntity;
        }
        
        public string GenerateModuleId()
        {
            Random r = new Random();
            int randNum = r.Next(100000);
            string fiveDigitNumber = randNum.ToString("D5");
            return fiveDigitNumber;
        }
    }
}