using ForestApp.Controllers.Requests;
using ForestApp.Data;
using ForestApp.Services;
using ForestApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ForestApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserService _userService;

    public AuthController(ApplicationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.CreateUser(request.Email, request.Password, "USER");

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        request.Password = "";
        return Ok(request);
    }
    
    [HttpPost]
    [Route("register-admin")]
    public async Task<ActionResult> RegisterAdmin(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.CreateUser(request.Email, request.Password, "ROLE_ADMIN");

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        request.Password = "";
        return Ok(request);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userService.GetUserByEmail(request.Email);
        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await _userService.IsPasswordCorrect(managedUser, request.Password);
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }
        
        var userInDb = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
        if (userInDb == null) return Unauthorized();
        if (userInDb.UserName == null || userInDb.Email == null) return BadRequest("Username or Email in db is null");
        
        var accessToken = TokenUtils.CreateToken(userInDb);
        await _dbContext.SaveChangesAsync();
        return Ok(new AuthResponse
        {
            Username = userInDb.UserName,
            Email = userInDb.Email,
            Token = accessToken,
        });
    }
}