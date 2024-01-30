namespace MRA.BlazorComponents.HttpClient.Responses;

// ReSharper disable once ClassNeverInstantiated.Global
public class ErrorResponse
{
    // ReSharper disable once CollectionNeverUpdated.Global
    public Dictionary<string, string[]> Errors { get; set; } = new();
}
