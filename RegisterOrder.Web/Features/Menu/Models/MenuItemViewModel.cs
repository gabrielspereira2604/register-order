namespace RegisterOrder.Web.Features.Menu.Models;

public sealed record MenuItemViewModel(
    int Id,
    string Name,
    decimal Price,
    MenuItemType Type
);
