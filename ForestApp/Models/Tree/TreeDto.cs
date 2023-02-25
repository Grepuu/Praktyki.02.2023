namespace ForestApp.Models.Tree;

public class TreeDto
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LeafDescription { get; set; }
    public double Height { get; set; }
    public int forestId { get; set; }
}