using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column("ProgrammeYears")]
        public int ProgrammeDuration { get; set; } 
        
        [Required]
        public int ProgrammeModuleNo{ get; set; } 
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<Student> Students { get; set; }
        public Programme()
        {
            Modules = new HashSet<Module>();
            Students = new HashSet<Student>();
        }
    }
}