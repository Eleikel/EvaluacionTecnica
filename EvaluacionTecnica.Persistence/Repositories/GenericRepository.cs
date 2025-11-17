using EvaluacionTecnica.Persistence.Context;
using EvaluacionTecnica.Persistence.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        public ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entity> CreateAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await Save();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            return await Save();
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _dbContext.Set<Entity>().AsNoTracking().ToListAsync();
        }

        public async Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public virtual async Task<bool> UpdateAsync(Entity entity, int id)
        {
            var entry = await _dbContext.Set<Entity>().FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            return await Save();
        }
    }
}
