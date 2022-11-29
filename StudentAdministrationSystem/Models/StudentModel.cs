using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class StudentModel
    {
        public string StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string StudentYear { get; set; }
        [Required]
        public string ProgrammeId { get; set; }
        public ProgrammeModel Programme { get; set; }
        public ICollection<GradeModel> Grade { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public StudentModel()
        {
            Programme = new ProgrammeModel();
            Grade = new HashSet<GradeModel>();
        }

        public StudentModel(Student student)
        {
            if (student == null) return;
            StudentId = student.StudentId;
            StudentName = student.StudentName;
            StudentYear = student.StudentYear;
            ProgrammeId = student.ProgrammeId;
            CreatedDate = student.CreatedDate;
            Programme = new ProgrammeModel();
            Grade = new HashSet<GradeModel>();
        }

        public Student Create(StudentModel studentModel)
        {
            return new Student
            {
               StudentId = GenerateStudentId(studentModel.StudentYear),
               StudentName = studentModel.StudentName,
               StudentYear = studentModel.StudentYear,
               ProgrammeId = studentModel.ProgrammeId,
               CreatedDate = DateTime.Now
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
        
        public string GenerateStudentId(string studentYear)
        {
            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");
            return studentYear + sixDigitNumber;
        }
    }
}