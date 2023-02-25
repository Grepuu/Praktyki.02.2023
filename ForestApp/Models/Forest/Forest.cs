using ForestApp.Models.Animal;
using ForestApp.Models.Tree;
using Microsoft.AspNetCore.Identity;

namespace ForestApp.Models.Forest;

public interface IForest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    
    public IdentityUser Owner { get; set; }
    public string OwnerId { get; set; }
    
    public ICollection<TreeEntity> Trees { get; set; }
    public ICollection<AnimalEntity> Animals { get; set; }
}