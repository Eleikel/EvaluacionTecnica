using EvaluacionTecnica.Business.Interfaces.Common;
using EvaluacionTecnica.Business.ViewModels.Roles;
using EvaluacionTecnica.Business.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.Interfaces.Service
{
    public interface IAuthService
    {
        Task<UserViewModel?> LoginAsync(AuthViewModel auth);
    }
}
