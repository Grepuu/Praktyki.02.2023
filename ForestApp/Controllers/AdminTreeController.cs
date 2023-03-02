
using ForestApp.Models.Tree;
using ForestApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForestApp.Controllers;

[ApiController]
[Route("api/admin/tree")]
[Authorize(Roles="ROLE_ADMIN")]
public class AdminTreeController : Controller
{
    private readonly ITreeService _treeService;

    public AdminTreeController(ITreeService treeService)
    {
        _treeService = treeService;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTree(int id)
    {
        var tree = await _treeService.GetTreeById(id);
        if (tree == null)
        {
            return NotFound();
        }

        await _treeService.DeleteTree(tree);
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTree(int id)
    {
        try
        {
            var tree = await _treeService.GetTreeById(id);
            if (tree == null)
            {
                return NotFound();
            }

            return Ok(new TreeDto()
            {
                DateAdded = tree.DateAdded,
                Description = tree.Description,
                Height = tree.Height,
                Id = tree.Id,
                LeafDescription = tree.LeafDescription,
                Name = tree.Name,
                forestId = tree.Forest.Id
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}