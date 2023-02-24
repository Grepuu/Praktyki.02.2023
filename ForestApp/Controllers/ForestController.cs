using System.Security.Claims;
using ForestApp.Controllers.Requests;
using ForestApp.Models.Forest;
using ForestApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForestController : Controller
{
    private readonly IForestService _forestService;
    private readonly UserManager<IdentityUser> _userManager;

    public ForestController(IForestService forestService, UserManager<IdentityUser> userManager)
    {
        _forestService = forestService;
        _userManager = userManager;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateForest([FromBody] CreateForestDto createForestDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId ?? throw new InvalidOperationException("You are not authenticated"));
        
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        var forest = new ForestEntity
        {
            Name = createForestDto.Name,
            Location = createForestDto.Location,
            Owner = user
        };

        await _forestService.AddForest(forest);

        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteForest(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var forest = await _forestService.GetForestById(id);

        if (forest == null)
        {
            return NotFound();
        }

        if (forest.OwnerId != userId)
        {
            return Unauthorized();
        }

        await _forestService.RemoveForest(forest.Id);

        return NoContent();
    }
}