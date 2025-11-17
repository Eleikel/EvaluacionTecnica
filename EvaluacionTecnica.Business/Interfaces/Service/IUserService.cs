using EvaluacionTecnica.Business.Interfaces.Common;
using EvaluacionTecnica.Business.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.Interfaces.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAll();
        Task<UserViewModel> GetById(int id);
        Task<UserViewModel> Create(UserViewModel entity);
        Task<UserViewModel> Update(int id, UserViewModel entity);
        Task<bool> Delete(int id);
    }
}
