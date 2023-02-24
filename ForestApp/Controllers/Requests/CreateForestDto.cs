using System.ComponentModel.DataAnnotations;

namespace ForestApp.Controllers.Requests;

public class CreateForestDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Location { get; set; } = null!;
}