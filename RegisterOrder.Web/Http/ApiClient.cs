using System.Net.Http.Json;

namespace RegisterOrder.Web.Http;

public sealed class ApiClient(HttpClient httpClient)
{
    public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(path, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await ReadAsync<T>(response, cancellationToken);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string path,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync(path, body, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await ReadAsync<TResponse>(response, cancellationToken);
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(
        string path,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PutAsJsonAsync(path, body, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await ReadAsync<TResponse>(response, cancellationToken);
    }

    public async Task DeleteAsync(string path, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.DeleteAsync(path, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    private static async Task<T> ReadAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken)
            ?? throw new InvalidOperationException("A API retornou uma resposta vazia.");
    }
}
