namespace ForestApp.Models.Animal;

public class AnimalDto
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int HerdSize { get; set; }
    public bool IsEndangered { get; set; }
    public int ForestId { get; set; }
}