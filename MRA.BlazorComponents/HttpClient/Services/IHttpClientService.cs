using MRA.BlazorComponents.HttpClient.Responses;

namespace MRA.BlazorComponents.HttpClient.Services;

public interface IHttpClientService
{
    Task<ApiResponse<T>> GetAsJsonAsync<T>(string url, object content = null);
    Task<ApiResponse> DeleteAsync(string url);
    Task<ApiResponse<T>> PostAsJsonAsync<T>(string url, Object content);
    Task<ApiResponse<T>> PutAsJsonAsync<T>(string url, object content);
    Task<ApiResponse> PutAsync(string url, object content);
}