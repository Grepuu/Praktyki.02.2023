using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace;

public class AnimalEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime DataDodania { get; set; } = DateTime.Now;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int NumberInHerd { get; set; }
    public bool IfEndangered { get; set; }
}
