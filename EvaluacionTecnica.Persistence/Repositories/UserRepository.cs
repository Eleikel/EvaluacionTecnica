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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public override async Task<bool> UpdateAsync(User entity, int id)
        {
            var entry = await _dbContext.Set<User>().FindAsync(id);
            var existingTransactionUser = entry.TransactionUser;
            var existingTransactionDate = entry.TransactionDate;

            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            entry.TransactionUser = existingTransactionUser;
            entry.TransactionDate = existingTransactionDate;

            return await Save();
        }


        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Set<User>()
                                    .Include(x => x.Role)
                                    .AsNoTracking()
                                    .ToListAsync();
        }
    }
}
