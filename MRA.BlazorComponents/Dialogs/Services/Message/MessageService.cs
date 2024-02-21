using Microsoft.Extensions.Configuration;
using MRA.BlazorComponents.Configuration;
using MRA.BlazorComponents.HttpClient.Responses;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.Identity.Application.Contract.Messages.Commands;
using MRA.Identity.Application.Contract.Messages.Responses;

namespace MRA.BlazorComponents.Dialogs.Services.Message
{
    public class MessageService(IHttpClientService httpClient, IConfiguration configuration) : IMessageService
    {
        public async Task<List<GetMessageResponse>> GetAllMessagesAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<GetMessageResponse>>(configuration.GetIdentityUrl("message"));
            return result.Result;
        }

        public async Task<ApiResponse> SendMessageAsync(SendMessageCommand command)
        {
            var result = await httpClient.PostAsJsonAsync(configuration.GetIdentityUrl("message"), command);
            return result;
        }
    }

}
