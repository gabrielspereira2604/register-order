namespace RegisterOrder.API.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = [];

    public decimal Subtotal => Items.Sum(i => i.MenuItem.Price);
    public decimal DiscountPercent => CalculateDiscount();
    public decimal Discount => Subtotal * DiscountPercent;
    public decimal Total => Subtotal - Discount;

    private decimal CalculateDiscount()
    {
        bool hasSandwich = Items.Any(i => i.MenuItem.Type == Domain.Enums.MenuItemType.Sandwich);
        bool hasSide = Items.Any(i => i.MenuItem.Type == Domain.Enums.MenuItemType.Side);
        bool hasDrink = Items.Any(i => i.MenuItem.Type == Domain.Enums.MenuItemType.Drink);

        if (hasSandwich && hasSide && hasDrink) return 0.20m;
        if (hasSandwich && hasDrink) return 0.15m;
        if (hasSandwich && hasSide) return 0.10m;
        return 0m;
    }
}
