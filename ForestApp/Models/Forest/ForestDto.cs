using ForestApp.Models.Animal;
using ForestApp.Models.Permission;
using ForestApp.Models.Tree;

namespace ForestApp.Models.Forest;

public class ForestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string OwnerId { get; set; }

    public List<TreeDto> Trees { get; set; }
    public List<AnimalDto> Animals { get; set; }
    public List<PermissionDto> Permissions { get; set; }
}