using Microsoft.Extensions.DependencyInjection;
using MRA.BlazorComponents.HttpClient.Services;

namespace MRA.BlazorComponents.HttpClient;

public static class DependencyInitializer
{
    public static IServiceCollection AddHttpClientService(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<IHttpClientService, HttpClientService>();
        return services;
    }
}