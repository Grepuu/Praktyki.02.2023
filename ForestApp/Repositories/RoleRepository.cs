using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForestApp.Repositories;

public interface IRoleRepository
{
    Task<List<IdentityRole>> GetAllRoles();
    Task<IdentityRole> GetRoleById(string id);
    Task<IdentityResult> CreateRole(string roleName);
    Task<IdentityResult> DeleteRole(string id);
}

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<IdentityRole>> GetAllRoles()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<IdentityRole> GetRoleById(string id)
    {
        return await _roleManager.FindByIdAsync(id) ?? throw new InvalidOperationException("Role not found");
    }

    public async Task<IdentityResult> CreateRole(string roleName)
    {
        var role = new IdentityRole(roleName);
        return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> DeleteRole(string id)
    {
        var role = await GetRoleById(id);
        return await _roleManager.DeleteAsync(role);
    }
}