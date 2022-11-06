
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using StudentAdministrationSystem.data;

namespace StudentAdministrationSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StudentAdministrationSystem.data.DataEntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        
        protected override void Seed(DataEntityContext context)
        {
            base.Seed(context);
            SeedData.SeedColleges(context); 
        }
    } 
}


