using Microsoft.EntityFrameworkCore;
using RegisterOrder.API.Data;
using RegisterOrder.API.Domain.Entities;

namespace RegisterOrder.API.Repositories;

public class MenuRepository(AppDbContext context) : IMenuRepository
{
    public async Task<IEnumerable<MenuItem>> GetAllAsync()
        => await context.MenuItems.ToListAsync();

    public async Task<MenuItem?> GetByIdAsync(int id)
        => await context.MenuItems.FindAsync(id);
}
