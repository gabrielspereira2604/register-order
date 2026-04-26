namespace RegisterOrder.API.DTOs;

public record PagedResult<T>(
    IEnumerable<T> Data,
    int Page,
    int PageSize,
    int TotalCount,
    decimal TotalRevenue = 0m
)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
};
