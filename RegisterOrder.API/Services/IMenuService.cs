using RegisterOrder.API.DTOs;

namespace RegisterOrder.API.Services;

public interface IMenuService
{
    Task<IEnumerable<MenuItemResponse>> GetAllAsync();
}
