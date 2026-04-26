namespace RegisterOrder.Web.Features.Menu.Models;

public sealed record DiscountRuleViewModel(
    int Percent,
    string Description,
    IReadOnlyList<string> Icons
);
