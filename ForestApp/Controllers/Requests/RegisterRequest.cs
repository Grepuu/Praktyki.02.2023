using System.ComponentModel.DataAnnotations;

namespace ForestApp.Controllers.Requests;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}