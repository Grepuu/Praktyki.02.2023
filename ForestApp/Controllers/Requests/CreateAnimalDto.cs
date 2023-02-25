using System.ComponentModel.DataAnnotations;

namespace ForestApp.Controllers.Requests;

public class CreateAnimalDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int HerdSize { get; set; }
    
    [Required]
    public bool IsEndangered { get; set; }
}