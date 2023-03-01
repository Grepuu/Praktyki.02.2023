﻿using System.Security.Claims;
using ForestApp.Controllers.Requests;
using ForestApp.Models.Animal;
using ForestApp.Models.Forest;
using ForestApp.Models.Permission;
using ForestApp.Models.Tree;
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
            return Forbid();
        }

        await _forestService.RemoveForest(forest.Id);

        return Ok();
    }
    
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetForest(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var forest = await _forestService.GetForestById(id);
        
        if (forest == null)
        {
            return NotFound();
        }
        if (forest.OwnerId != userId)
        {
            return Forbid();
        }

        return Ok(ForestEntity.ToForestDto(forest));
    }
    
    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> GetAllUserForests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Forbid();
        }
        
        var forests = await _forestService.GetAllUserForests(userId);
        var forestDtos = new List<ForestDto>();

        foreach (var forest in forests)
        {
            var forestDto = ForestEntity.ToForestDto(forest);
            forestDtos.Add(forestDto);
        }

        return Ok(forestDtos);
    }
    
    [HttpPost("{forestId:int}/add-tree")]
    [Authorize]
    public async Task<IActionResult> AddTreeToForest(int forestId, [FromBody] CreateTreeDto createTreeDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var forest = await _forestService.GetForestById(forestId);

        if (forest == null)
        {
            return NotFound("Forest not found");
        }

        if (forest.OwnerId != userId)
        {
            return Forbid("You are not the owner of this forest");
        }

        var tree = new TreeEntity
        {
            Name = createTreeDto.Name,
            Description = createTreeDto.Description,
            LeafDescription = createTreeDto.LeafDescription,
            Height = createTreeDto.Height,
            Forest = forest
        };

        await _forestService.AddTreeToForest(forestId, tree);

        return Ok();
    }
    
    [HttpPost("{forestId:int}/add-animal")]
    [Authorize]
    public async Task<IActionResult> AddAnimalToForest(int forestId, [FromBody] CreateAnimalDto createAnimalDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var forest = await _forestService.GetForestById(forestId);

        if (forest == null)
        {
            return NotFound("Forest not found");
        }

        if (forest.OwnerId != userId)
        {
            return Forbid("You are not the owner of this forest");
        }

        var animal = new AnimalEntity()
        {
            Name = createAnimalDto.Name,
            Description = createAnimalDto.Description,
            HerdSize = createAnimalDto.HerdSize,
            IsEndangered = createAnimalDto.IsEndangered,
            Forest = forest
        };

        await _forestService.AddAnimalToForest(forestId, animal);

        return Ok();
    }
    
    [HttpPost("{forestId:int}/add-permission")]
    [Authorize]
    public async Task<IActionResult> AddPermissionToForest(int forestId, [FromBody] CreatePermissionDto createPermissionDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var forest = await _forestService.GetForestById(forestId);

        if (forest == null)
        {
            return NotFound("Forest not found");
        }

        if (forest.OwnerId != userId)
        {
            return Forbid("You are not the owner of this forest");
        }

        var permission = new PermissionEntity()
        {
            Title = createPermissionDto.Title,
            Description = createPermissionDto.Description,
            DateFrom = createPermissionDto.DateFrom,
            DateTo = createPermissionDto.DateTo,
            Forest = forest
        };

        await _forestService.AddPermissionToForest(forestId, permission);

        return Ok();
    }
}