using ForestApp.Models.Forest;

namespace ForestApp.Models.Animal;

public interface IAnimal
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int HerdSize { get; set; }
    public bool IsEndangered { get; set; }
    
    public ForestEntity Forest { get; set; }
}