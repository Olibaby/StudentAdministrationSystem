using System.Data.Common;
using System.Data.Entity;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.data
{
    public class DataEntityContext: DbContext
    {
        public DataEntityContext() : base("name=StudentAdminDBConnectionString")
        {
            // Database.SetInitializer<DataEntityContext>(new CreateDatabaseIfNotExists<DataEntityContext>());
            //Database.SetInitializer<DataEntityContext>(new DropCreateDatabaseIfModelChanges<DataEntityContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataEntityContext, Migrations.Configuration>());
        }
        
        public DbSet<Programme> Programmes { get; set; }
    }
}