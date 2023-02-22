using ForestApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForestApp.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public ActionResult GetAllRoles()
    {
        var roles = _roleService.GetAllRoles();
        return Ok(roles.Result);
    }

    [HttpGet($"{{id}}")]
    public async Task<ActionResult> GetRoleById(string id)
    {
        try
        {
            var role = await _roleService.GetRoleById(id);
            return Ok(role);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateRole(string roleName)
    {
        var result = await _roleService.CreateRole(roleName);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete($"{{id}}")]
    public async Task<ActionResult> DeleteRole(string id)
    {
        try
        {
            var result = await _roleService.DeleteRole(id);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}