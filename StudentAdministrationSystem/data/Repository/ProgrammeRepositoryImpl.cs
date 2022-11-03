using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;

namespace StudentAdministrationSystem.data.Repository
{
    public class ProgrammeRepositoryImpl: IProgrammeRepository
    {
        private DataEntityContext _context;

        public ProgrammeRepositoryImpl()
        {
            _context = new DataEntityContext();
        }
        public IEnumerable<Programme> GetProgrammes()
        {
            return _context.Set<Programme>().ToList();
        }

        public Programme GetProgrammeById(string programmeId)
        {
            return _context.Set<Programme>().Find(programmeId);
        }

        public IQueryable<Programme> GetProgramme(params Expression<Func<Programme, object>>[] includeProperties)
        {
            IQueryable<Programme> query = _context.Set<Programme>();
            foreach( var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void AddProgramme(Programme programme)
        {
            _context.Set<Programme>().Add(programme);
            // _context.SaveChanges();
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }

        public void UpdateProgramme(string programmeId, Programme programme)
        {
            if(_context.Entry(programme).State  == EntityState.Detached)
            {
                _context.Set<Programme>().Attach(programme);
            }
            _context.Entry(programme).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveProgramme(string programmeId)
        {
            var item = GetProgrammeById(programmeId);
            _context.Set<Programme>().Remove(item);
            _context.SaveChanges();
        }
    }
}