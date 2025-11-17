using AutoMapper;
using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.ViewModels.Users;
using EvaluacionTecnica.Persistence.Entities;
using EvaluacionTecnica.Persistence.Interfaces.Repository;


namespace EvaluacionTecnica.Business.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserViewModel> Create(UserViewModel entity)
        {
            User userEntity = _mapper.Map<User>(entity);
            userEntity = await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<UserViewModel>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            return await _userRepository.DeleteAsync(userEntity);
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            var userEntities = await _userRepository.GetAllAsync();
            var userEntitiesViewModel = _mapper.Map<IEnumerable<UserViewModel>>(userEntities);
            return userEntitiesViewModel;
        }

        public async Task<UserViewModel> GetById(int id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserViewModel>(userEntity);

        }

        public async Task<UserViewModel> Update(int id, UserViewModel entity)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            await _userRepository.UpdateAsync(_mapper.Map<User>(entity), id);
            return await GetById(id);
        }
    }
}
