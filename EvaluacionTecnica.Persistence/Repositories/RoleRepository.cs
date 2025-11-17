using EvaluacionTecnica.Persistence.Context;
using EvaluacionTecnica.Persistence.Entities;
using EvaluacionTecnica.Persistence.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<bool> UpdateAsync(Role entity, int id)
        {
            var entry = await _dbContext.Set<Role>().FindAsync(id);
            var existingTransactionUser = entry.TransactionUser; 
            var existingTransactionDate = entry.TransactionDate;

            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            entry.TransactionUser = existingTransactionUser; 
            entry.TransactionDate = existingTransactionDate;

            return await Save();
        }


        public override async Task<bool> DeleteAsync(Role entity)
        {

            var tieneUsuarios = await _dbContext.Users.AnyAsync(u => u.RoleId == entity.Id);

            if (tieneUsuarios)
                throw new InvalidOperationException("No puedes eliminar este rol porque tiene usuarios asignados.");

            var role = await GetByIdAsync(entity.Id);
            _dbContext.Set<Role>().Remove(entity);
            return await Save();
        }

    }
}
