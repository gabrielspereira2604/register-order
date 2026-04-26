using RegisterOrder.API.Domain.Enums;

namespace RegisterOrder.API.DTOs;

public record MenuItemResponse(int Id, string Name, decimal Price, MenuItemType Type);
