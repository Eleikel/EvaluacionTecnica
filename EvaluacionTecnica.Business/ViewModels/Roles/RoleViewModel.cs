using AutoMapper;
using EvaluacionTecnica.Business.Mappings.Base;
using EvaluacionTecnica.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.ViewModels.Roles
{
    public class RoleViewModel : IMapFrom<Role>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Rol requerido.")]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RoleViewModel, Role>()
                   .ForMember(dest => dest.TransactionDate, opt => opt.Ignore())
                   .ForMember(dest => dest.TransactionUser, opt => opt.Ignore())
                   .ReverseMap();
        }
    }
}
