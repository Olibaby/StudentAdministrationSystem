
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace StudentAdministrationSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StudentAdministrationSystem.data.DataEntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}