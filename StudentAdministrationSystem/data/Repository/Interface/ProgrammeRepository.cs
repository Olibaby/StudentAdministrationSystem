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
        IQueryable<Programme> Get<Programme>(params Expression<Func<Programme, object>>[] includeProperties);
        void Add(Programme programme);
        void Update(string id,Programme programme);
        void Remove(int id);
    }
}