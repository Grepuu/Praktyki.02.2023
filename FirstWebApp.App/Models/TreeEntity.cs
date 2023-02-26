using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace;

public class TreeEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LeafDescription { get; set; }
    public int MaxHeight { get; set; }
}
