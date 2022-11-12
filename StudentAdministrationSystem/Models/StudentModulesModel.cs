using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class StudentModulesModel
    {
            public int StudentModuleId { get; set; }
        
            public string ModuleId { get; set; }
            public Module Module { get; set; }
        
            public string StudentId { get; set; }
            public Student Student { get; set; }
    }
}