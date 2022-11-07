using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentAdministrationSystem.data.Entities
{
    public class Student
    {
        [Key]
        [MaxLength(10)]
        public string StudentId { get; set; }
        
        [Required]
        public string StudentName { get; set; }
        
        [Required]
        public string StudentYear { get; set; }
        public string ProgrammeId { get; set; }
        public Programme Programme { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<Grade> Grade { get; set; }

        public Student()
        {
            Grade = new HashSet<Grade>();
        }
    }
}