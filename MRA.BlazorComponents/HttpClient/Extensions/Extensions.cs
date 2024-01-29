using System.Net.Http.Json;

namespace MRA.BlazorComponents.HttpClient.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> GetFromJsonAsync<T>(this System.Net.Http.HttpClient httpClient, string route, object query)
    {
        string queryString = string.Join("&", query.GetType().GetProperties()
            .Select(property => $"{property.Name}={property.GetValue(query)}"));
        return (await httpClient.GetFromJsonAsync<T>($"{route}?{queryString}"))!;
    }
}
