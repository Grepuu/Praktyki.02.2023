using ForestApp.Models.Forest;

namespace ForestApp.Models.Permission;

public class PermissionEntity : IPermission
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    
    public ForestEntity Forest { get; set; }
}