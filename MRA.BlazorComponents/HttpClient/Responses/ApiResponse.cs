using System.Net;
using System.Net.Http.Json;

namespace MRA.BlazorComponents.HttpClient.Responses;

public record ApiResponse
{
    public bool Success { get; set; }
    public HttpStatusCode? HttpStatusCode { get; set; }
    public BadRequestResponse? BadRequestResponse { get; protected set; }

    public static async Task<ApiResponse> BuildFromHttpResponse(HttpResponseMessage responseMessage)
    {
        var apiResponse = new ApiResponse
        {
            Success = true,
            HttpStatusCode = responseMessage.StatusCode
        };
        if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            apiResponse.BadRequestResponse = await responseMessage.Content.ReadFromJsonAsync<BadRequestResponse>();
        }

        return apiResponse;
    }
}

public record ApiResponse<T> : ApiResponse
{
    public T? Result { get; private set; }

    public static async Task<ApiResponse<T>> BuildFromHttpResponseAsync(HttpResponseMessage responseMessage)
    {
        var apiResponse = new ApiResponse<T>
        {
            HttpStatusCode = responseMessage.StatusCode,
            Success = true
        };

        if (responseMessage.IsSuccessStatusCode)
            apiResponse.Result = await responseMessage.Content.ReadFromJsonAsync<T>();
        else
            apiResponse.BadRequestResponse = await responseMessage.Content.ReadFromJsonAsync<BadRequestResponse>();

        return apiResponse;
    }
}