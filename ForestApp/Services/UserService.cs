using ForestApp.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ForestApp.Services;

public interface IUserService
{
    Task<IdentityUser?> GetUserByEmail(string email);
    Task<IdentityResult> CreateUser(string email, string password);
    Task<bool> IsPasswordCorrect(IdentityUser identityUser, string password);
}

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityUser?> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> CreateUser(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<bool> IsPasswordCorrect(IdentityUser identityUser, string password)
    {
        return await _userManager.CheckPasswordAsync(identityUser, password);
    }
}