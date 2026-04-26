using RegisterOrder.API.DTOs;
using RegisterOrder.API.Repositories;

namespace RegisterOrder.API.Services;

public class MenuService(IMenuRepository repository) : IMenuService
{
    public async Task<IEnumerable<MenuItemResponse>> GetAllAsync()
    {
        var items = await repository.GetAllAsync();
        return items.Select(i => new MenuItemResponse(i.Id, i.Name, i.Price, i.Type));
    }
}
