namespace ForestApp.Models.Permission;

public class PermissionDto
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int ForestId { get; set; }
}