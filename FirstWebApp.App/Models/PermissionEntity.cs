using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace;

public class PermissionEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime SinceWhen { get; set; }
    public DateTime UntilWhen { get; set; }
}