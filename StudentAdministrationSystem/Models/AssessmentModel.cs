using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class AssessmentModel
    {
        public int AssessmentId { get; set; }
        [Required]
        public string AssessmentTitle { get; set; }
        [Required]
        public string AssessmentMaxScore { get; set; }
        [Required]
        public string ModuleId { get; set; }
        public ModuleModel Module { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<GradeModel> Grade { get; set; }

        public AssessmentModel()
        {
            Module = new ModuleModel();
            Grade = new HashSet<GradeModel>();
        }

        public AssessmentModel(Assessment assessment)
        {
            if (assessment == null) return;
            AssessmentId = assessment.AssessmentId;
            AssessmentTitle = assessment.AssessmentTitle;
            AssessmentMaxScore = assessment.AssessmentMaxScore;
            ModuleId = assessment.ModuleId;
            Module = new ModuleModel();
            CreatedDate = assessment.CreatedDate;
            Grade = new HashSet<GradeModel>();
        }

        public Assessment Create(AssessmentModel assessmentModel)
        {
            return new Assessment
            {
                AssessmentId = assessmentModel.AssessmentId,
                AssessmentTitle = assessmentModel.AssessmentTitle,
                AssessmentMaxScore = assessmentModel.AssessmentMaxScore,
                ModuleId = assessmentModel.ModuleId,
                CreatedDate = DateTime.Now
            };
        }

        public Assessment Edit(Assessment assessmentEntity, AssessmentModel assessmentModel)
        {
            assessmentEntity.AssessmentId = assessmentModel.AssessmentId;
            assessmentEntity.AssessmentTitle = assessmentModel.AssessmentTitle;
            assessmentEntity.AssessmentMaxScore = assessmentModel.AssessmentMaxScore;
            assessmentEntity.ModuleId = assessmentModel.ModuleId;
            assessmentEntity.ModifiedDate = assessmentModel.ModifiedDate;
            return assessmentEntity;
        }
    }
}