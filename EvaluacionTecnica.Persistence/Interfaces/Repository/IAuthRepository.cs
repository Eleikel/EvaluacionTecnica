using EvaluacionTecnica.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence.Interfaces.Repository
{
    public interface IAuthRepository
    {
        Task<User?> LogInAsync(User user);

    }
}
