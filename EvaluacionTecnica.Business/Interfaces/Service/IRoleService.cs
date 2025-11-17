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
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetAll();
        Task<RoleViewModel> GetById(int id);
        Task<RoleViewModel> Create(RoleViewModel entity);
        Task<RoleViewModel> Update(int id, RoleViewModel entity);
        Task<bool> Delete(int id);
    }
}
