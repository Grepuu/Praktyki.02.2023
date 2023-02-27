namespace ForestApp.Controllers.Requests;

public class UpdatePermissionDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}