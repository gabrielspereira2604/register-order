using RegisterOrder.Web.Features.Orders.Models;
using RegisterOrder.Web.Http;

namespace RegisterOrder.Web.Features.Orders.Services;

public sealed class OrdersApiService(ApiClient apiClient)
{
    public Task<OrderViewModel> GetOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<OrderViewModel>($"api/orders/{id}", cancellationToken);
    }

    public Task<PagedResultViewModel<OrderViewModel>> GetOrdersAsync(
        int page = 1,
        int pageSize = 5,
        CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PagedResultViewModel<OrderViewModel>>(
            $"api/orders?page={page}&pageSize={pageSize}",
            cancellationToken);
    }

    public Task<OrderViewModel> CreateOrderAsync(
        OrderRequestViewModel request,
        CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<OrderRequestViewModel, OrderViewModel>(
            "api/orders",
            request,
            cancellationToken);
    }

    public Task<OrderViewModel> UpdateOrderAsync(
        int id,
        OrderRequestViewModel request,
        CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync<OrderRequestViewModel, OrderViewModel>(
            $"api/orders/{id}",
            request,
            cancellationToken);
    }

    public Task DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync($"api/orders/{id}", cancellationToken);
    }
}
