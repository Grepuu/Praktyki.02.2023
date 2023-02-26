using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace;

public class TreeEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string LeafDescription { get; set; } = null!;
    public int MaxHeight { get; set; }
}
