using ForestApp.Models.Forest;

namespace ForestApp.Models.Tree;

public interface ITree
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LeafDescription { get; set; }
    public double Height { get; set; }
    
    public ForestEntity Forest { get; set; }
}