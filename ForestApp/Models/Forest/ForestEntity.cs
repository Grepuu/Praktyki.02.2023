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

    public static ForestDto ToForestDto(ForestEntity forest)
    {
        var treesDto = forest.Trees.Select(tree => new TreeDto()
        {
            Id = tree.Id,
            DateAdded = tree.DateAdded,
            Description = tree.Description,
            Height = tree.Height,
            LeafDescription = tree.LeafDescription,
            Name = tree.Name,
            forestId = forest.Id
        }).ToList();

        var animalsDto = forest.Animals.Select(animal => new AnimalDto()
        {
            Id = animal.Id,
            DateAdded = animal.DateAdded,
            Description = animal.Description,
            Name = animal.Name,
            IsEndangered = animal.IsEndangered,
            HerdSize = animal.HerdSize,
            ForestId = forest.Id,
        }).ToList();
        
        var permissionsDto = forest.Permissions.Select(permission => new PermissionDto()
        {
            Id = permission.Id,
            DateAdded = permission.DateAdded,
            Description = permission.Description,
            DateFrom = permission.DateFrom,
            DateTo = permission.DateTo,
            ForestId = forest.Id,
        }).ToList();
        
        return new ForestDto()
        {
            Id = forest.Id,
            Location = forest.Location,
            Name = forest.Name,
            OwnerId = forest.OwnerId,
            Trees = treesDto,
            Animals = animalsDto,
            Permissions = permissionsDto
        };
    }
}