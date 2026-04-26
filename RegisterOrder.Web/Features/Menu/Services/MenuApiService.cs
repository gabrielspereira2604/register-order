using RegisterOrder.Web.Features.Menu.Models;
using RegisterOrder.Web.Http;

namespace RegisterOrder.Web.Features.Menu.Services;

public sealed class MenuApiService(ApiClient apiClient)
{
    public async Task<IReadOnlyList<MenuItemViewModel>> GetMenuAsync(CancellationToken cancellationToken = default)
    {
        var items = await apiClient.GetAsync<List<MenuItemViewModel>>("api/menu", cancellationToken);
        return items;
    }
}
