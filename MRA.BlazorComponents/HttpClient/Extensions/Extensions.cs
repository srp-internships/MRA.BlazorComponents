namespace MRA.BlazorComponents.HttpClient.Extensions;

public static class UrlExtensions
{
    public static string AppendQuery(this string route, object query)
    {
        string queryString = string.Join("&", query.GetType().GetProperties()
            .Select(property => $"{property.Name}={property.GetValue(query)}"));
        return $"{route}?{queryString}";
    }
}