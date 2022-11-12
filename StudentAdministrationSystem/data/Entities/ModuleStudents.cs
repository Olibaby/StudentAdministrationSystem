using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Models
{
    public class ModuleStudent
    {
        [Key]
        public string ModuleStudentId { get; set; }
        
        public string ModuleId { get; set; }
        public Module Module { get; set; }
        
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}