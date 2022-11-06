using System;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class GradeModel
    {
        public int GradeId { get; set; }
        public decimal Mark { get; set; }
        public string StudentId { get; set; }
        public StudentModel Student { get; set; }
        public int AssessmentId { get; set; }
        public AssessmentModel Assessment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public GradeModel()
        {
            Student = new StudentModel();
            Assessment = new AssessmentModel();
        }
        
        public GradeModel(Grade grade)
        {
            GradeId = grade.GradeId;
            Mark = grade.Mark;
            StudentId = grade.StudentId;
            Student = new StudentModel();
            AssessmentId = grade.AssessmentId;
            Assessment = new AssessmentModel();
        }

        public Grade Create(Grade grade)
        {
            return new Grade
            {
                GradeId = grade.GradeId,
                Mark = grade.Mark,
                StudentId = grade.StudentId,
                AssessmentId = grade.AssessmentId,
                CreatedDate = grade.CreatedDate
            };
        }
        
        public Grade Edit(Grade gradeEntity, GradeModel gradeModel)
        {
            gradeEntity.GradeId = gradeModel.GradeId;
            gradeEntity.Mark = gradeModel.Mark;
            gradeEntity.StudentId = gradeModel.StudentId;
            gradeEntity.AssessmentId = gradeModel.AssessmentId;
            gradeEntity.ModifiedDate = gradeModel.ModifiedDate;
            return gradeEntity;
        }
    }
}