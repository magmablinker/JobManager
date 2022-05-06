using JobManager.Core.Data.Model;
using JobManager.Core.Database;
using JobManager.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Repository.Base
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly JobManagerContext _context;
        private DbSet<T> _entities;

        public GenericRepository(JobManagerContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<bool> Delete(T value)
        {
            return await Task.Factory.StartNew(() =>
            {
                bool isDeleted = false;

                if (_entities.Contains(value))
                {
                    _entities.Remove(value);
                    isDeleted = true;
                }

                return isDeleted;
            });
        }

        public async Task<List<T>> SelectAll()
        {
            return await _entities
                            .Select(x => x)
                            .ToListAsync();
        }

        public async Task<T> SelectById(Guid id)
        {
            return await _entities
                            .FindAsync(id);
        }

        public async Task<T> Insert(T value)
        {
            return await Task.Factory.StartNew(() =>
            {
                _entities.Add(value);
                return value;
            });
        }

        public async Task<bool> Update(T value)
        {
            return await Task.Factory.StartNew(() =>
            {
                bool isUpdated = false;

                if (_context.Entry(value).State == EntityState.Detached) _entities.Attach(value);
                _context.Entry(value).State = EntityState.Modified;

                isUpdated = true;

                return isUpdated;
            });
        }

        public async Task SaveChangesAsync()
        {
            var entries = _context.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseModel && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseModel)entityEntry.Entity).ModifiedOn = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseModel)entityEntry.Entity).CreatedOn = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
