using Microsoft.Extensions.DependencyInjection;
using MRA.BlazorComponents.Dialogs.Services.Message;

namespace MRA.BlazorComponents.Dialogs;

public static class DependencyInitializer
{
    public static IServiceCollection AddDialogs(this IServiceCollection services) =>
        services.AddScoped<IMessageService, MessageService>();
}