using AutoMapper;
using EvaluacionTecnica.Business.Mappings.Base;
using EvaluacionTecnica.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.ViewModels.Roles
{
    public class AuthViewModel : IMapFrom<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, AuthViewModel>().ReverseMap();
        }
    }
}
