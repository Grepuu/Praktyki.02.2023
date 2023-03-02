using ForestApp.Models.Forest;

namespace ForestApp.Models.Animal;

public class AnimalEntity : IAnimal
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int HerdSize { get; set; }
    public bool IsEndangered { get; set; }
    
    public ForestEntity Forest { get; set; }
    public int ForestId { get; set; }
}