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
    public class AuthRepository : IAuthRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public AuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> LogInAsync(User user)
        {
            if(user == null) return null;

            return await _dbContext.Users
                                   .Include(x => x.Role)
                                   .FirstOrDefaultAsync(u => u.UserName == user.UserName
                                                         && u.Password == user.Password);
       }
    }
}
