using System;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class GradeModel
    {
        // public int GradeId { get; set; }
        // public decimal Mark { get; set; }
        // public string StudentId { get; set; }
        // public StudentModel Student { get; set; }
        // public int AssessmentId { get; set; }
        // public AssessmentModel Assessment { get; set; }
        
        public int GradeId { get; set; }
        public decimal Mark { get; set; }
        public string StudentId { get; set; }
        public StudentModel Student { get; set; }
        public int AssessmentId { get; set; }
        public AssessmentModel Assessment { get; set; }
        public string ModuleId { get; set; }
        public ModuleModel Module { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public GradeModel()
        {
            Student = new StudentModel();
            Assessment = new AssessmentModel();
            Module = new ModuleModel();
        }
        
        public GradeModel(Grade grade)
        {
            if (grade == null) return;
            GradeId = grade.GradeId;
            Mark = grade.Mark;
            StudentId = grade.StudentId;
            Student = new StudentModel();
            Assessment = new AssessmentModel();
            Module = new ModuleModel();
            CreatedDate = grade.CreatedDate;
        }

        public Grade Create(GradeModel grade)
        {
            return new Grade
            {
                GradeId = grade.GradeId,
                Mark = grade.Mark,
                StudentId = grade.StudentId,
                AssessmentId = grade.AssessmentId,
                ModuleId = grade.ModuleId,
                CreatedDate = DateTime.Now
            };
        }
        
        public Grade Edit(Grade gradeEntity, GradeModel gradeModel)
        {
            gradeEntity.GradeId = gradeModel.GradeId;
            gradeEntity.Mark = gradeModel.Mark;
            gradeEntity.StudentId = gradeModel.StudentId;
            gradeEntity.AssessmentId = gradeModel.AssessmentId;
            gradeEntity.ModuleId = gradeModel.ModuleId;
            gradeEntity.ModifiedDate = gradeModel.ModifiedDate;
            return gradeEntity;
        }
    }
}