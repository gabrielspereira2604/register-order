namespace RegisterOrder.Web.Features.Orders.Models;

public sealed record OrderViewModel(
    int Id,
    DateTime CreatedAt,
    IReadOnlyList<OrderItemViewModel> Items,
    decimal Subtotal,
    decimal DiscountPercent,
    decimal Discount,
    decimal Total
);
