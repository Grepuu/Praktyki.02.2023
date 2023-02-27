using System.ComponentModel.DataAnnotations;

namespace ForestApp.Controllers.Requests;

public class CreatePermissionDto
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
}