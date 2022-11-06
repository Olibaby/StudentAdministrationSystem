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
        public ProgrammeModel Programme { get; set; }
        public ICollection<AssessmentModel> Assessment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        

        public ModuleModel()
        {
            Programme = new ProgrammeModel();
            Assessment = new HashSet<AssessmentModel>();
        }

        public ModuleModel(Module module)
        {
            ModuleId = module.ModuleId;
            ModuleTitle = module.ModuleTitle;
            ModuleType = module.ModuleType;
            ProgrammeId = module.ProgrammeId;
            Programme = new ProgrammeModel();
            Assessment = new HashSet<AssessmentModel>();
            CreatedDate = module.CreatedDate;
        }

        public Module Create(ModuleModel moduleModel)
        {
            return new Module
            {
                ModuleId = moduleModel.ModuleId,
                ModuleTitle = moduleModel.ModuleTitle,
                ModuleType = moduleModel.ModuleType,
                ProgrammeId = moduleModel.ProgrammeId,
                CreatedDate = moduleModel.CreatedDate
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
    }
}