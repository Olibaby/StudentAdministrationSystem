using System.Collections.Generic;
using System.Linq;
using StudentAdministrationSystem.data;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.Migrations
{
    public class SeedData
    {
        public static List<Programme> SeedColleges(DataEntityContext context)
        {
            var programmes = new List<Programme>
            {
                new Programme() { ProgrammeId = "101111", ProgrammeDuration = "One Year", ProgrammeTitle = "Advanced Computer Science"}
            };
            programmes.ForEach(programme =>
            {
                if (!context.Programmes.Where(p => p.ProgrammeId == programme.ProgrammeId).Any())
                    context.Programmes.Add(programme);
            });
            context.SaveChanges();
            return programmes;
        }
    }
}