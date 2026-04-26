namespace RegisterOrder.API.DTOs;

public record OrderResponse(
    int Id,
    DateTime CreatedAt,
    List<OrderItemResponse> Items,
    decimal Subtotal,
    decimal DiscountPercent,
    decimal Discount,
    decimal Total
);
