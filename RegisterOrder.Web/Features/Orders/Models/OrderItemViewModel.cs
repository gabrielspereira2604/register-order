namespace RegisterOrder.Web.Features.Orders.Models;

public sealed record OrderItemViewModel(
    int MenuItemId,
    string Name,
    decimal Price
);
