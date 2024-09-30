using B2B_Project.Application.Features.Message.Commands.SendMessage;

namespace B2B_Project.Application.Services
{
    public interface IChatHubService
    {
        Task<bool> SendMessageAsync(SendMessageCommandRequest request);
    }
}
