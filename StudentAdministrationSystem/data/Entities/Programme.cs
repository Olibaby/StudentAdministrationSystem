using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAdministrationSystem.data.Entities
{
    public class Programme
    {
        [Key]
        [MaxLength(6)]
        public string ProgrammeId { get; set; }
        
        [Required]
        public string ProgrammeTitle { get; set; }
        
        [Required]
        public string ProgrammeDuration { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}