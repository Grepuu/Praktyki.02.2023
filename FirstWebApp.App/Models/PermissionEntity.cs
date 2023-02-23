using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.App.Models
{
    public class PermissionEntity
    {
        [Key]
        public int IdTree { get; set; }
        public DateTime TreeDateAdded { get; set; }
        public string? TreeName { get; set; }
        public string? TreeDescription { get; set; } 
        public string? LeafDescription { get; set; }
        public float MaxHeight { get; set; }

    }
}
