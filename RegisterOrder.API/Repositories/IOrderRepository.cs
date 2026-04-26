using RegisterOrder.API.Domain.Entities;

namespace RegisterOrder.API.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync(int page, int pageSize);
    Task<int> CountAsync();
    Task<decimal> GetTotalRevenueAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<Order> CreateAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task DeleteAsync(Order order);
}
