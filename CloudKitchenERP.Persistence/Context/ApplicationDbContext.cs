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
    public DbSet<Cart> Carts => Set<Cart>();

    public DbSet<CartItem> CartItems => Set<CartItem>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
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

        modelBuilder.Entity<Cart>()
    .HasOne(c => c.User)
    .WithOne(u => u.Cart)
    .HasForeignKey<Cart>(c => c.UserId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.MenuItem)
            .WithMany(m => m.CartItems)
            .HasForeignKey(ci => ci.MenuItemId);

        modelBuilder.Entity<CartItem>()
            .Property(ci => ci.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CartItem>()
            .Property(ci => ci.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
    .HasOne(o => o.User)
    .WithMany(u => u.Orders)
    .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany(m => m.OrderItems)
            .HasForeignKey(oi => oi.MenuItemId);

        modelBuilder.Entity<Order>()
            .Property(o => o.SubTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.DeliveryCharge)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.Tax)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.GrandTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(o => o.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(o => o.TotalPrice)
            .HasPrecision(18, 2);
    }
}
