using Microsoft.AspNetCore.Identity;

namespace ForestApp.Services;

public interface IUserService
{
    Task<IdentityUser?> GetUserByEmail(string email);
    Task<IdentityResult> CreateUser(string email, string password, string roleName);
    Task<bool> IsPasswordCorrect(IdentityUser identityUser, string password);
}

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityUser?> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> CreateUser(string email, string password, string roleName)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            var role = new IdentityRole("USER");
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return result;
            }
        }
    
        var assignRoleResult = await _userManager.AddToRoleAsync(user, roleName);
        if (!assignRoleResult.Succeeded)
        {
            return assignRoleResult;
        }

        return await _userManager.CreateAsync(user, password);
    }

    public async Task<bool> IsPasswordCorrect(IdentityUser identityUser, string password)
    {
        return await _userManager.CheckPasswordAsync(identityUser, password);
    }
}