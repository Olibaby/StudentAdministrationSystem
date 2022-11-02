using System.Data.Entity.Migrations;

namespace StudentAdministrationSystem
{
    public class DatabaseConfig
    {
        public static void MigrateToLatest()
        {
            //Upgrade DB to Latest
            var configuration = new Migrations.Configuration
            {
                //Allow for Data Loss Migrations
                AutomaticMigrationDataLossAllowed = true
            };

            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}