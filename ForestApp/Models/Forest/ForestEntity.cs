using Microsoft.AspNetCore.Identity;

namespace ForestApp.Models.Forest;

public class ForestEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    
    public IdentityUser Owner { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
}