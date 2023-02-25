using System.Security.Claims;
using ForestApp.Models.Tree;
using ForestApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForestApp.Controllers;

[ApiController]
[Route("api/trees")]
[Authorize]
public class TreeController : Controller
{
    private readonly ITreeService _treeService;

    public TreeController(ITreeService treeService)
    {
        _treeService = treeService;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTree(int id)
    {
        var tree = await _treeService.GetTreeById(id);
        if (tree == null)
        {
            return NotFound();
        }
        
        if (tree.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return Forbid();
        }
        
        await _treeService.DeleteTree(tree);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTree(int id)
    {
        try
        {
            var tree = await _treeService.GetTreeById(id);
            if (tree == null)
            {
                return NotFound();
            }
            
            if (tree.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
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