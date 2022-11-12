using System.ComponentModel.DataAnnotations;

namespace StudentAdministrationSystem.data.Entities
{
    public class StudentModule
    {
        [Key]
        public string StudentModuleId { get; set; }
        
        public string ModuleId { get; set; }
        public Module Module { get; set; }
        
        public string StudentId { get; set; }
        public Student Student { get; set; }

    }
}