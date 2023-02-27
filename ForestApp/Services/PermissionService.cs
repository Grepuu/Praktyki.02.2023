using ForestApp.Models.Animal;
using ForestApp.Models.Permission;
using ForestApp.Repositories;

namespace ForestApp.Services;

public interface IPermissionService
{
    Task<PermissionEntity> CreatePermission(PermissionEntity animal);
    Task<PermissionEntity?> GetPermissionById(int id);
    Task UpdatePermission(PermissionEntity permission);
    Task DeletePermission(PermissionEntity permission);
}

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionService(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<PermissionEntity> CreatePermission(PermissionEntity permission)
    {
        await _permissionRepository.AddPermission(permission);
        return permission;
    }

    public async Task<PermissionEntity?> GetPermissionById(int id)
    {
        return await _permissionRepository.GetPermissionById(id);
    }

    public async Task UpdatePermission(PermissionEntity permission)
    {
        await _permissionRepository.UpdatePermission(permission);
    }

    public async Task DeletePermission(PermissionEntity permission)
    {
        await _permissionRepository.DeletePermission(permission);
    }
}