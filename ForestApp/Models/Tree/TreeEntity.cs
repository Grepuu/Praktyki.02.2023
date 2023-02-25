using ForestApp.Models.Forest;

namespace ForestApp.Models.Tree;

public class TreeEntity : ITree
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string LeafDescription { get; set; } = null!;
    public double Height { get; set; }
    
    public ForestEntity Forest { get; set; }
}