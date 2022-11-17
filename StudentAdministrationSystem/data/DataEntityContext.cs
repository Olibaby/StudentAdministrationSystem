using System.Data.Common;
using System.Data.Entity;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.data
{
    public class DataEntityContext: DbContext
    {
        public DataEntityContext() : base("name=StudentAdminDBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataEntityContext, Migrations.Configuration>());
        }
        
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Module> Modules { get; set; } 
        public DbSet<Assessment> Assessments { get; set; } 
        public DbSet<Student> Students { get; set; } 
        public DbSet<Grade> Grades { get; set; } 
        // public DbSet<ModuleStudent> ModuleStudents { get; set; }
        public DbSet<StudentModule> StudentModules { get; set; }
        
    }
}