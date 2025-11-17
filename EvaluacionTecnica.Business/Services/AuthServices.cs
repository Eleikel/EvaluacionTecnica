using AutoMapper;
using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.ViewModels.Roles;
using EvaluacionTecnica.Business.ViewModels.Users;
using EvaluacionTecnica.Persistence.Entities;
using EvaluacionTecnica.Persistence.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthServices(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<UserViewModel?> LoginAsync(AuthViewModel auth)
        {
            var userEntity = _mapper.Map<User>(auth);

            var user = await _authRepository.LogInAsync(userEntity);

            if (user == null)
                return null; 

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
