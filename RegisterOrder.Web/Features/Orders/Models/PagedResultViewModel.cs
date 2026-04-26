namespace RegisterOrder.Web.Features.Orders.Models;

public sealed record PagedResultViewModel<T>(
    IReadOnlyList<T> Data,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    decimal TotalRevenue = 0m
);
