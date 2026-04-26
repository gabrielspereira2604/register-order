using RegisterOrder.API.Domain.Entities;

namespace RegisterOrder.API.Repositories;

public interface IMenuRepository
{
    Task<IEnumerable<MenuItem>> GetAllAsync();
    Task<MenuItem?> GetByIdAsync(int id);
}
