using ForestApp.Controllers.Requests;
using ForestApp.Data;
using ForestApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly TokenService _tokenService;
    
    public AuthController(UserManager<IdentityUser> userManager, ApplicationDbContext dbContext, TokenService tokenService)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _tokenService = tokenService;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _userManager.CreateAsync(
            new IdentityUser { UserName = request.Username, Email = request.Email},
            request.Password
        );
        if (result.Succeeded)
        {
            request.Password = "";
            return CreatedAtAction(nameof(Register), new {email = request.Email}, request);
        }
        foreach (var error in result.Errors) {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return BadRequest(ModelState);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userManager.FindByEmailAsync(request.Email);
        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }
        var userInDb = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
        if (userInDb == null)
        {
            return Unauthorized();
        }
        if (userInDb.UserName == null || userInDb.Email == null)
        {
            return BadRequest("Username or Email in db is null");
        }
        var accessToken = _tokenService.CreateToken(userInDb);
        await _dbContext.SaveChangesAsync();
        return Ok(new AuthResponse
        {
            Username = userInDb.UserName,
            Email = userInDb.Email,
            Token = accessToken,
        });
    }
}