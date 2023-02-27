using ForestApp.Data;
using ForestApp.Models.Permission;
using Microsoft.EntityFrameworkCore;

namespace ForestApp.Repositories;

public interface IPermissionRepository
{
    Task<PermissionEntity?> GetPermissionById(int id);
    Task AddPermission(PermissionEntity permission);
    Task UpdatePermission(PermissionEntity permission);
    Task DeletePermission(PermissionEntity permission);
}

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PermissionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PermissionEntity?> GetPermissionById(int id)
    {
        return await _dbContext.Permissions
            .Include(a => a.Forest)
            .FirstOrDefaultAsync(t => t.Id == id) ?? throw new InvalidOperationException("Permission not found");;
    }

    public async Task AddPermission(PermissionEntity permission)
    {
        await _dbContext.Permissions.AddAsync(permission);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePermission(PermissionEntity permission)
    {
        _dbContext.Permissions.Update(permission);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePermission(PermissionEntity permission)
    {
        _dbContext.Permissions.Remove(permission);
        await _dbContext.SaveChangesAsync();
    }
}
