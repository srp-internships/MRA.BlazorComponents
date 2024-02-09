using System.Net;
using MRA.BlazorComponents.HttpClient.Responses;
using MudBlazor;

namespace MRA.BlazorComponents.Snackbar.Extensions;

public static class SnackbarExtensions
{
    public static void ShowIfError(this ISnackbar snackbar, ApiResponse apiResponse, string notResponding,
        string? successMessage = null)
    {
        if (!apiResponse.Success)
        {
            snackbar.Add(notResponding, Severity.Error);
            return;
        }

        if (apiResponse.HttpStatusCode == HttpStatusCode.BadRequest)
        {
            snackbar.Add(apiResponse.BadRequestResponse?.Detail);
            foreach (var (key, value) in apiResponse.BadRequestResponse?.Errors ??
                                         new Dictionary<string, List<string>>())
            {
                snackbar.Add($"{key}: {value.Aggregate("", (o, p) => o + ", " + p)}", Severity.Error);
            }

            return;
        }

        if (apiResponse.HttpStatusCode is HttpStatusCode.Conflict or HttpStatusCode.Unauthorized)
        {
            snackbar.Add(apiResponse.BadRequestResponse?.Detail);
            return;
        }

        if (successMessage != null && apiResponse.HttpStatusCode == HttpStatusCode.OK)
        {
            snackbar.Add(successMessage, Severity.Success);
        }
    }
}