using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace;

public class AnimalEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime DataDodania { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberInHerd { get; set; }
    public bool IfEndangered { get; set; }
}
