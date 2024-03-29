using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAdministrationSystem.data.Entities
{
    public class Grade
    {
        // [Key]
        // public int GradeId { get; set; }
        // [Required]
        // public decimal Mark { get; set; }
        // public string StudentId { get; set; }
        // public Student Student { get; set; }
        // public int AssessmentId { get; set; }
        // public Assessment Assessment { get; set; }
        
        [Key]
        public int GradeId { get; set; }
        [Required]
        public decimal Mark { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public string ModuleId { get; set; }
        public Module Module { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Grade()
        {
            
        }
    }
}