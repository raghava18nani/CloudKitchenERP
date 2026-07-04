using CloudKitchenERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudKitchenERP.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MenuItem>()
    .HasOne(m => m.Category)
    .WithMany(c => c.MenuItems)
    .HasForeignKey(m => m.CategoryId);

        modelBuilder.Entity<MenuItem>()
    .Property(m => m.Price)
    .HasPrecision(18, 2);

        modelBuilder.Entity<Customer>()
    .HasOne(c => c.User)
    .WithOne(u => u.Customer)
    .HasForeignKey<Customer>(c => c.UserId);
    }
}
