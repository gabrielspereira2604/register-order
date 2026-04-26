using Microsoft.EntityFrameworkCore;
using RegisterOrder.API.Data;
using RegisterOrder.API.Domain.Entities;

namespace RegisterOrder.API.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllAsync(int page, int pageSize)
        => await context.Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.MenuItem)
            .OrderByDescending(o => o.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<int> CountAsync()
        => await context.Orders.CountAsync();

    public async Task<decimal> GetTotalRevenueAsync()
    {
        var orders = await context.Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.MenuItem)
            .ToListAsync();
        return orders.Sum(o => o.Total);
    }

    public async Task<Order?> GetByIdAsync(int id)
        => await context.Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<Order> CreateAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(Order order)
    {
        context.Orders.Remove(order);
        await context.SaveChangesAsync();
    }
}
