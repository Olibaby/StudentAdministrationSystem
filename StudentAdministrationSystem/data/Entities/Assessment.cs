using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentAdministrationSystem.data.Entities
{
    public class Assessment
    {
        [Key]
        public int AssessmentId { get; set; }
        
        [Required]
        public string AssessmentTitle { get; set; }
        
        [Required]
        public string AssessmentMaxScore { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModuleId { get; set; }
        public Module Module { get; set; }
        public ICollection<Grade> Grade { get; set; }

        public Assessment()
        {
            Grade = new HashSet<Grade>();
        }
    }
}