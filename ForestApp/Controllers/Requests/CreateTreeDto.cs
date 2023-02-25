using System.ComponentModel.DataAnnotations;

namespace ForestApp.Controllers.Requests;

public class CreateTreeDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string LeafDescription { get; set; }
    
    [Required]
    public double Height { get; set; }
}