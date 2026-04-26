using RegisterOrder.API.Domain.Entities;
using RegisterOrder.API.DTOs;
using RegisterOrder.API.Exceptions;
using RegisterOrder.API.Repositories;

namespace RegisterOrder.API.Services;

public class OrderService(IOrderRepository orderRepository, IMenuRepository menuRepository) : IOrderService
{
    public async Task<PagedResult<OrderResponse>> GetAllAsync(int page, int pageSize)
    {
        var orders = await orderRepository.GetAllAsync(page, pageSize);
        var total = await orderRepository.CountAsync();
        var totalRevenue = await orderRepository.GetTotalRevenueAsync();
        return new PagedResult<OrderResponse>(orders.Select(MapToResponse), page, pageSize, total, totalRevenue);
    }

    public async Task<OrderResponse> GetByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id)
            ?? throw new OrderNotFoundException(id);
        return MapToResponse(order);
    }

    public async Task<OrderResponse> CreateAsync(OrderRequest request)
    {
        var items = await ResolveAndValidateItemsAsync(request.MenuItemIds);
        var order = new Order { Items = items };
        var created = await orderRepository.CreateAsync(order);
        return MapToResponse(created);
    }

    public async Task<OrderResponse> UpdateAsync(int id, OrderRequest request)
    {
        var order = await orderRepository.GetByIdAsync(id)
            ?? throw new OrderNotFoundException(id);

        var items = await ResolveAndValidateItemsAsync(request.MenuItemIds);
        order.Items = items;

        var updated = await orderRepository.UpdateAsync(order);
        return MapToResponse(updated);
    }

    public async Task DeleteAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id)
            ?? throw new OrderNotFoundException(id);
        await orderRepository.DeleteAsync(order);
    }

    private async Task<List<OrderItem>> ResolveAndValidateItemsAsync(List<int> menuItemIds)
    {
        var duplicateId = menuItemIds.GroupBy(x => x).FirstOrDefault(g => g.Count() > 1)?.Key;
        if (duplicateId is not null)
            throw new DuplicateItemException($"Item {duplicateId} informado mais de uma vez.");

        var items = new List<OrderItem>();
        foreach (var menuItemId in menuItemIds)
        {
            var menuItem = await menuRepository.GetByIdAsync(menuItemId)
                ?? throw new ArgumentException($"Item de cardápio {menuItemId} não encontrado.");
            items.Add(new OrderItem { MenuItemId = menuItemId, MenuItem = menuItem });
        }

        var duplicateType = items
            .GroupBy(i => i.MenuItem.Type)
            .FirstOrDefault(g => g.Count() > 1)?.Key;

        if (duplicateType is not null)
            throw new DuplicateItemException($"Apenas um item do tipo '{duplicateType}' é permitido por pedido.");

        return items;
    }

    private static OrderResponse MapToResponse(Order order) => new(
        order.Id,
        order.CreatedAt,
        order.Items.Select(i => new OrderItemResponse(i.MenuItemId, i.MenuItem.Name, i.MenuItem.Price)).ToList(),
        order.Subtotal,
        order.DiscountPercent,
        order.Discount,
        order.Total
    );
}
