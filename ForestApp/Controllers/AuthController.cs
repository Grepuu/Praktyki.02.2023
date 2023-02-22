using ForestApp.Controllers.Requests;
using ForestApp.Data;
using ForestApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly TokenService _tokenService;
    private readonly IUserService _userService;

    public UserController(ApplicationDbContext dbContext, TokenService tokenService, IUserService userService)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
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

        var result = await _userService.CreateUser(request.Email, request.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        request.Password = "";
        return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
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