using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.data.Entities
{
    public class Module
    {
        [Key]
        [MaxLength(5)]
        public string ModuleId { get; set; }
        
        [Required]
        public string ModuleTitle { get; set; }
        
        [Required]
        public string ModuleType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ProgrammeId { get; set; }
        public Programme Programme { get; set; }
        public ICollection<Assessment> Assessment { get; set; }
        
        public Module()
        {
            Assessment = new HashSet<Assessment>();
            Programme = new Programme();
        }
    }
}