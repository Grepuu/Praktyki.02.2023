using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.App.Models
{
    public class AnimalEntity
    {
        [Key]
        public int IdAnimal { get; set; }
        public DateTime AnimalDateAdded { get; set; }
        public string? AnimalName { get; set; }
        public string? AnimalDescription { get; set; } 
        public int NumberOfIndividuals { get; set; }
        public bool Endangered { get; set; }
    }
}
