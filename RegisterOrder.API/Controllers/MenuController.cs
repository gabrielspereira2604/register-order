using Microsoft.AspNetCore.Mvc;
using RegisterOrder.API.Services;

namespace RegisterOrder.API.Controllers;

[ApiController]
[Route("api/menu")]
public class MenuController(IMenuService menuService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await menuService.GetAllAsync();
        return Ok(items);
    }
}
