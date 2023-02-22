using ForestApp.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ForestApp.Services
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllRoles();
        Task<IdentityRole> GetRoleById(string id);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityResult> DeleteRole(string id);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<List<IdentityRole>> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await _roleRepository.GetRoleById(id);
        }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            return await _roleRepository.CreateRole(roleName);
        }

        public async Task<IdentityResult> DeleteRole(string id)
        {
            return await _roleRepository.DeleteRole(id);
        }
    }
}