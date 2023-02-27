using ForestApp.Models.Animal;
using ForestApp.Models.Permission;
using ForestApp.Models.Tree;
using Microsoft.AspNetCore.Identity;

namespace ForestApp.Models.Forest;

public class ForestEntity : IForest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public IdentityUser Owner { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
    
    public ICollection<TreeEntity> Trees { get; set; } = new List<TreeEntity>();
    public ICollection<AnimalEntity> Animals { get; set; } = new List<AnimalEntity>();
    public ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
}