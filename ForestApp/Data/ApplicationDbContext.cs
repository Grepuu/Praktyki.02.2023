using ForestApp.Models.Animal;
using ForestApp.Models.Forest;
using ForestApp.Models.Permission;
using ForestApp.Models.Tree;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForestApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<ForestEntity> Forests { get; set; } = null!;
    public DbSet<TreeEntity> Trees { get; set; } = null!;
    public DbSet<AnimalEntity> Animals { get; set; } = null!;
    public DbSet<PermissionEntity> Permissions { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<ForestEntity>()
            .HasKey(f => f.Id);
        modelBuilder.Entity<ForestEntity>()
            .Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<ForestEntity>()
            .Property(f => f.Location)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<ForestEntity>()
            .HasOne(f => f.Owner)
            .WithMany()
            .HasForeignKey(f => f.OwnerId);
    }
}