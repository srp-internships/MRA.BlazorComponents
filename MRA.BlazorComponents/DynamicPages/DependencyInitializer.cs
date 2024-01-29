using Microsoft.Extensions.DependencyInjection;

namespace MRA.BlazorComponents.DynamicPages;

public static class DependencyInitializer
{
    public static IServiceCollection AddMraPages(this IServiceCollection services)
    {
        services.AddHttpClient("identity", client => client.BaseAddress = new Uri(""));
    }
}