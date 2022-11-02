using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;

namespace StudentAdministrationSystem.data.Repository.Interface
{
    public interface IProgrammeRepository
    {
        IEnumerable<Programme> GetProgrammes();
        Programme GetProgrammeById(string programmeId);  
        IQueryable<Programme> GetProgramme(params Expression<Func<Programme, object>>[] includeProperties);
        void AddProgramme(Programme programme);
        void UpdateProgramme(string programmeId,Programme programme);
        void RemoveProgramme(string programmeId);
    }
}