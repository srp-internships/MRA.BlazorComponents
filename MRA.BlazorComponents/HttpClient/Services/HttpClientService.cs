using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using MRA.BlazorComponents.HttpClient.Extensions;
using MRA.BlazorComponents.HttpClient.Responses;
using MRA.Identity.Application.Contract.User.Responses;

namespace MRA.BlazorComponents.HttpClient.Services;

public class HttpClientService(
    IHttpClientFactory httpClientFactory,
    ILocalStorageService localStorageService) : IHttpClientService
{
    public async Task<ApiResponse<T>> GetFromJsonAsync<T>(string url, object? content = null)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            if (content == null)
            {
                var response = await httpClient.GetAsync(url);
                return await ApiResponse<T>.BuildFromHttpResponseAsync(response);
            }

            return await ApiResponse<T>.BuildFromHttpResponseAsync(await httpClient.GetAsync(url.AppendQuery(content)));
        }
        catch (HttpRequestException)
        {
            return new ApiResponse<T>();
        }
    }

    public async Task<ApiResponse> GetAsync(string url)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var httpResponseMessage = await httpClient.GetAsync(url);
            return await ApiResponse.BuildFromHttpResponse(httpResponseMessage);
        }
        catch (HttpRequestException)
        {
            return new ApiResponse();
        }
    }

    public async Task<ApiResponse> DeleteAsync(string url)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var response = await httpClient.DeleteAsync(url);
            return await ApiResponse.BuildFromHttpResponse(response);
        }
        catch (HttpRequestException)
        {
            return new ApiResponse();
        }
    }

    public async Task<ApiResponse<T>> PostAsJsonAsync<T>(string url, object content)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var response = await httpClient.PostAsJsonAsync(url, content);
            return await ApiResponse<T>.BuildFromHttpResponseAsync(response);
        }
        catch (HttpRequestException)
        {
            return new ApiResponse<T>();
        }
    }

    public async Task<ApiResponse> PostAsJsonAsync(string url, object content)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var response = await httpClient.PostAsJsonAsync(url, content);
            return await ApiResponse.BuildFromHttpResponse(response);
        }
        catch (HttpRequestException)
        {
            return new ApiResponse();
        }
    }

    public async Task<ApiResponse<T>> PutAsJsonAsync<T>(string url, object content)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var response = await httpClient.PutAsJsonAsync(url, content);
            return await ApiResponse<T>.BuildFromHttpResponseAsync(response);
        }
        catch (HttpRequestException)
        {
            return new ApiResponse<T>();
        }
    }

    public async Task<ApiResponse> PutAsJsonAsync(string url, object content)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var response = await httpClient.PutAsJsonAsync(url, content);
            return new ApiResponse
            {
                Success = response.IsSuccessStatusCode,
                HttpStatusCode = response.StatusCode
            };
        }
        catch (HttpRequestException)
        {
            return new ApiResponse();
        }
    }

    public async Task<ApiResponse<T>> PatchAsJsonAsync<T>(string url, object content)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var response = await httpClient.PatchAsJsonAsync(url, content);
            return await ApiResponse<T>.BuildFromHttpResponseAsync(response);
        }
        catch (HttpRequestException)
        {
            return new ApiResponse<T>();
        }
    }

    public async Task<ApiResponse> PatchAsJsonAsync(string url, object content)
    {
        try
        {
            using var httpClient = await CreateHttpClient();
            var response = await httpClient.PatchAsJsonAsync(url, content);
            return new ApiResponse
            {
                Success = response.IsSuccessStatusCode,
                HttpStatusCode = response.StatusCode
            };
        }
        catch (HttpRequestException)
        {
            return new ApiResponse();
        }
    }

    private async Task<System.Net.Http.HttpClient> CreateHttpClient()
    {
        var httpClient = httpClientFactory.CreateClient();
        var authToken = await localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        if (authToken != null!)
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authToken.AccessToken);
        return httpClient;
    }
}