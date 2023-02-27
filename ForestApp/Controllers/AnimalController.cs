using ForestApp.Models.Animal;
using ForestApp.Services;

namespace ForestApp.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/animals")]
[Authorize]
public class AnimalController : Controller
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await _animalService.GetAnimalById(id);
        if (animal == null)
        {
            return NotFound();
        }

        if (animal.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return Forbid();
        }

        await _animalService.DeleteAnimal(animal);
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAnimal(int id)
    {
        try
        {
            var animal = await _animalService.GetAnimalById(id);
            if (animal == null)
            {
                return NotFound();
            }

            if (animal.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            return Ok(new AnimalDto()
            {
                DateAdded = animal.DateAdded,
                Description = animal.Description,
                HerdSize = animal.HerdSize,
                Id = animal.Id,
                IsEndangered = animal.IsEndangered,
                Name = animal.Name,
                ForestId = animal.Forest.Id
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("{id:int}/isEndangered")]
    public async Task<IActionResult> ToggleEndangered(int id)
    {
        var animal = await _animalService.GetAnimalById(id);
        if (animal == null)
        {
            return NotFound();
        }

        if (animal.Forest.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return Forbid();
        }

        animal.IsEndangered = !animal.IsEndangered;
        await _animalService.UpdateAnimal(animal);
        
        return Ok(new AnimalDto()
        {
            Id = animal.Id,
            DateAdded = animal.DateAdded,
            Name = animal.Name,
            Description = animal.Description,
            HerdSize = animal.HerdSize,
            IsEndangered = animal.IsEndangered,
            ForestId = animal.Forest.Id
        });
    }
}