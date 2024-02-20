using MRA.BlazorComponents.HttpClient.Responses;
using MRA.Identity.Application.Contract.Messages.Commands;
using MRA.Identity.Application.Contract.Messages.Responses;

namespace MRA.BlazorComponents.DynamicPages.Services.Message
{
    public interface IMessageService
    {
        Task<ApiResponse> SendMessageAsync(SendMessageCommand command);
        Task<List<GetMessageResponse>> GetAllMessagesAsync();
    }
}
