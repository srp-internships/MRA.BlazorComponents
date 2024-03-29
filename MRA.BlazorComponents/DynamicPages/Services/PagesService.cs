using Microsoft.Extensions.Configuration;
using MRA.BlazorComponents.Configuration;
using MRA.BlazorComponents.HttpClient.Services;

namespace MRA.BlazorComponents.DynamicPages.Services;

public class PagesService(IHttpClientService httpClient, IConfiguration configuration) : IPagesService
{
    public async Task<string> GetHtmlContentAsync(string pageName)
    {
        var lang = Environmets.ApplicationCulture.Name;
        var pagesResponse =
            await httpClient.GetFromJsonAsync<ContentResponse>(
                configuration.GetPagesUrl($"api/pages/getContent?lang={lang}&pageName={pageName}"));
        return pagesResponse.Success ? pagesResponse.Result.HtmlContent : "";
    }

    public async Task<List<PageResponse>> GetPagesAsync(string? application = null)
    {
        var lang = Environmets.ApplicationCulture.Name;
        var pagesResponse =
            await httpClient.GetFromJsonAsync<List<PageResponse>>(configuration.GetPagesUrl($"api/pages?lang={lang}&application={application}"));
        return pagesResponse.Success ? pagesResponse.Result : [];
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class PageResponse
{
    public string Title { get; set; }
    public string Name { get; set; }
    public bool ShowInMenu { get; set; }
}

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ContentResponse
{
    public string HtmlContent { get; set; }
}