using Microsoft.Extensions.Configuration;

namespace MRA.BlazorComponents.Configuration;

public static class Extensions
{
    public static string GetJobsUrl(
        this IConfiguration configuration, string? url = null)
    {
        return configuration["HttpClients:Jobs"] + url;
    }

    public static string GetIdentityUrl(
        this IConfiguration configuration, string? url = null)
    {
        return configuration["HttpClients:Identity"] + url;
    }
    
    public static string GetPagesUrl(
        this IConfiguration configuration, string? url = null)
    {
        return configuration["HttpClients:Pages"] + url;
    }
}