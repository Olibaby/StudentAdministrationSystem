using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;

namespace StudentAdministrationSystem.data.Repository
{
    public class ProgrammeRepositoryImpl: IProgrammeRepository
    {
        public IEnumerable<Programme> GetProgrammes()
        {
            throw new NotImplementedException();
        }

        public Programme GetProgrammeById(string programmeId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Programme1> Get<Programme1>(params Expression<Func<Programme1, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Add(Programme programme)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Programme programme)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}