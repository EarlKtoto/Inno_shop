using Inno_Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Infrastructure.Data;

public class Inno_ShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.User)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UserId);
        
        base.OnModelCreating(modelBuilder);
    }
}