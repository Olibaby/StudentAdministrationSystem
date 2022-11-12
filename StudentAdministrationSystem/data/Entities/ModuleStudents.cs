using System;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class ModuleStudentModel
    {
        public string ModuleId { get; set; }
        public string StudentId { get; set; }
        public Module Module { get; set; }
        public Student Student { get; set; }

        public ModuleStudentModel()
        {
            
        }
    }
}