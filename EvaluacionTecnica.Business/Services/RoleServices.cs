using AutoMapper;
using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.ViewModels.Roles;
using EvaluacionTecnica.Persistence.Entities;
using EvaluacionTecnica.Persistence.Interfaces.Repository;


namespace EvaluacionTecnica.Business.Services
{
    public class RoleServices : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public readonly IMapper _mapper;
        public RoleServices(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<RoleViewModel> Create(RoleViewModel entity)
        {
            Role roleEntity = _mapper.Map<Role>(entity);
            roleEntity = await _roleRepository.CreateAsync(roleEntity);
            return _mapper.Map<RoleViewModel>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var roleEntity = await _roleRepository.GetByIdAsync(id);
            return await _roleRepository.DeleteAsync(roleEntity);
        }

        public async Task<IEnumerable<RoleViewModel>> GetAll()
        {
            var roleEntities = await _roleRepository.GetAllAsync();
            var roleEntitiesViewModel = _mapper.Map<IEnumerable<RoleViewModel>>(roleEntities);
            return roleEntitiesViewModel;
        }

        public async Task<RoleViewModel> GetById(int id)
        {
            var roleEntity = await _roleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleViewModel>(roleEntity);

        }

        public async Task<RoleViewModel> Update(int id, RoleViewModel entity)
        {
            var roleEntity = await _roleRepository.GetByIdAsync(id);
            await _roleRepository.UpdateAsync(_mapper.Map<Role>(entity), id);
            return await GetById(id);
        }
    }
}
