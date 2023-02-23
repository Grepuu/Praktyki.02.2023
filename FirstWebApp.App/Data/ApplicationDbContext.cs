using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FirstWebApp.App.Models;

namespace FirstWebApp.App.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<FirstWebApp.App.Models.AnimalEntity> AnimalEntity { get; set; } = default!;
}