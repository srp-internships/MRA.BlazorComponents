using Microsoft.Extensions.DependencyInjection;
using MRA.BlazorComponents.DynamicPages.Services;

namespace MRA.BlazorComponents.DynamicPages;

public static class DependencyInitializer
{
    public static IServiceCollection AddMraPages(this IServiceCollection services) =>
        services.AddScoped<IPagesService, PagesService>();
}