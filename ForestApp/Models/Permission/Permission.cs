using ForestApp.Models.Forest;

namespace ForestApp.Models.Permission;

public interface IPermission
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    
    public ForestEntity Forest { get; set; }
}