using ForestApp.Controllers.Requests;
using ForestApp.Models.Permission;
using ForestApp.Services;

namespace ForestApp.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/permissions")]
[Authorize]
public class PermissionController : Controller
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePermission(int id)
    {
        var permission = await _permissionService.GetPermissionById(id);
        if (permission == null)
        {
            return NotFound();
        }

        if (permission.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return Forbid();
        }

        await _permissionService.DeletePermission(permission);
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPermission(int id)
    {
        try
        {
            var permission = await _permissionService.GetPermissionById(id);
            if (permission == null)
            {
                return NotFound();
            }

            if (permission.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            return Ok(new PermissionDto()
            {
                DateAdded = permission.DateAdded,
                Description = permission.Description,
                DateFrom = permission.DateFrom,
                DateTo = permission.DateTo,
                Id = permission.Id,
                Title = permission.Title,
                ForestId = permission.Forest.Id
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdatePermission(int id, [FromBody] UpdatePermissionDto dto)
    {
        var permission = await _permissionService.GetPermissionById(id);
        if (permission == null)
        {
            return NotFound();
        }

        if (permission.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return Forbid();
        }

        permission.Title = dto.Title;
        permission.Description = dto.Description;
        permission.DateFrom = dto.DateFrom;
        permission.DateTo = dto.DateTo;

        await _permissionService.UpdatePermission(permission);
        
        return Ok(new PermissionDto()
        {
            Id = permission.Id,
            DateAdded = permission.DateAdded,
            Title = permission.Title,
            Description = permission.Description,
            DateFrom = permission.DateFrom,
            DateTo = permission.DateTo,
            ForestId = permission.Forest.Id
        });
    }
}