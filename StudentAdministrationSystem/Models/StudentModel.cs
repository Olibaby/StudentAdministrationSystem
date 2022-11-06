using System;
using System.Collections.Generic;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class StudentModel
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentYear { get; set; }
        public string ProgrammeId { get; set; }
        public ProgrammeModel Programme { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<GradeModel> Grade { get; set; }

        public StudentModel()
        {
            Programme = new ProgrammeModel();
            Grade = new HashSet<GradeModel>();
        }

        public StudentModel(Student student)
        {
            StudentId = student.StudentId;
            StudentName = student.StudentName;
            StudentYear = student.StudentYear;
            ProgrammeId = student.ProgrammeId;
            Programme = new ProgrammeModel();
            CreatedDate = student.CreatedDate;
            Grade = new HashSet<GradeModel>();
        }

        public Student Create(StudentModel studentModel)
        {
            return new Student
            {
               StudentId = studentModel.StudentId,
               StudentName = studentModel.StudentName,
               StudentYear = studentModel.StudentYear,
               ProgrammeId = studentModel.ProgrammeId,
               CreatedDate = studentModel.CreatedDate
            };
        }

        public Student Edit(Student studentEntity, StudentModel studentModel)
        {
            studentEntity.StudentId = studentModel.StudentId;
            studentEntity.StudentName = studentModel.StudentName;
            studentEntity.StudentYear = studentModel.StudentYear;
            studentEntity.ProgrammeId = studentModel.ProgrammeId;
            studentEntity.ModifiedDate = studentModel.ModifiedDate;
            return studentEntity;
        }
        
        public string GenerateStudentId()
        {
            // Random r = new Random();
            // int randNum = r.Next(1000000);
            // string sixDigitNumber = randNum.ToString("D6");
            // return sixDigitNumber;
            return "";
        }
    }
}