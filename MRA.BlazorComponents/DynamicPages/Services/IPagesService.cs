namespace MRA.BlazorComponents.DynamicPages.Services;

public interface IPagesService
{
    Task<string> GetHtmlContentAsync(string pageName);
    Task<List<PageResponse>> GetPagesAsync(string? application = null);
}