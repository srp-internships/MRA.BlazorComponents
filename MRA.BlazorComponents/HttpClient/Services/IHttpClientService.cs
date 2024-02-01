using MRA.BlazorComponents.HttpClient.Responses;

namespace MRA.BlazorComponents.HttpClient.Services;

public interface IHttpClientService
{
    Task<ApiResponse<T>> GetFromJsonAsync<T>(string url, object? content = null);
    Task<ApiResponse> GetAsync(string url);
    Task<ApiResponse> DeleteAsync(string url);
    Task<ApiResponse<T>> PostAsJsonAsync<T>(string url, object content);
    Task<ApiResponse> PostAsJsonAsync(string url, object content);
    Task<ApiResponse<T>> PutAsJsonAsync<T>(string url, object content);
    Task<ApiResponse> PutAsJsonAsync(string url, object content);
}