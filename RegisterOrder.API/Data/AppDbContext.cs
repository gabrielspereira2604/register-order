using Microsoft.EntityFrameworkCore;
using RegisterOrder.API.Domain.Entities;
using RegisterOrder.API.Domain.Enums;

namespace RegisterOrder.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, Name = "X Burger", Price = 5.00m, Type = MenuItemType.Sandwich },
            new MenuItem { Id = 2, Name = "X Egg", Price = 4.50m, Type = MenuItemType.Sandwich },
            new MenuItem { Id = 3, Name = "X Bacon", Price = 7.00m, Type = MenuItemType.Sandwich },
            new MenuItem { Id = 4, Name = "Batata frita", Price = 2.00m, Type = MenuItemType.Side },
            new MenuItem { Id = 5, Name = "Refrigerante", Price = 2.50m, Type = MenuItemType.Drink }
        );
    }
}
