using Microsoft.Extensions.Configuration;
using MRA.BlazorComponents.Configuration;
using MRA.BlazorComponents.HttpClient.Responses;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.Identity.Application.Contract.Messages.Commands;
using MRA.Identity.Application.Contract.Messages.Responses;

namespace MRA.BlazorComponents.DynamicPages.Services.Message
{
    public class MessageService(IHttpClientService _httpClient, IConfiguration configuration) : IMessageService
    {
        public async Task<List<GetMessageResponse>> GetAllMessagesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GetMessageResponse>>(configuration.GetIdentityUrl("message"));
            return result.Result;
        }

        public async Task<ApiResponse> SendMessageAsync(SendMessageCommand command)
        {
            var result = await _httpClient.PostAsJsonAsync(configuration.GetIdentityUrl("message"), command);
            return result;
        }
    }

}
