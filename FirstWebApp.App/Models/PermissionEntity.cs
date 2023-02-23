using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.App.Models
{
    public class PermissionEntity
    {
        [Key]
        public int IdPermission { get; set; }
        public DateTime PermissionDateAdded { get; set; }
        public string? PermissionName { get; set; }
        public string? PermissionDescription { get; set; }
        public DateTime SinceWhen { get; set; }
        public DateTime UntilWhen { get; set; }

    }
}
