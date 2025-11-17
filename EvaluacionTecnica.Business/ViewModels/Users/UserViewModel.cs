using AutoMapper;
using EvaluacionTecnica.Business.Mappings.Base;
using EvaluacionTecnica.Business.ViewModels.Roles;
using EvaluacionTecnica.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.ViewModels.Users
{
    public class UserViewModel : IMapFrom<User>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El rol es requerido.")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El apellido es requerido.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "La cédula o ID es requerida.")]
        public long IdCard { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "La fecha de Nacimiento es requerida.")]
        public DateTime? Birthday { get; set; }
        public virtual RoleViewModel? Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
