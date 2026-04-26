namespace RegisterOrder.Web.Features.Orders.Models;

public sealed record OrderRequestViewModel(IReadOnlyList<int> MenuItemIds);
