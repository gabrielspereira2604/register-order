using RegisterOrder.API.DTOs;

namespace RegisterOrder.API.Services;

public interface IOrderService
{
    Task<PagedResult<OrderResponse>> GetAllAsync(int page, int pageSize);
    Task<OrderResponse> GetByIdAsync(int id);
    Task<OrderResponse> CreateAsync(OrderRequest request);
    Task<OrderResponse> UpdateAsync(int id, OrderRequest request);
    Task DeleteAsync(int id);
}
