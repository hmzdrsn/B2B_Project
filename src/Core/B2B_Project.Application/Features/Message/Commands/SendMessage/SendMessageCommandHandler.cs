using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Message.Commands.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommandRequest, HandlerResponse<SendMessageCommandResponse>>
    {
        private readonly IChatHubService _chatHubService;

        public SendMessageCommandHandler(IChatHubService chatHubService)
        {
            _chatHubService = chatHubService;
        }

        public async Task<HandlerResponse<SendMessageCommandResponse>> Handle(SendMessageCommandRequest request, CancellationToken cancellationToken)
        {
            bool res = await _chatHubService.SendMessageAsync(request);
            if (res)
            {
                return new()
                {
                    Message = "Message sent successfully!"
                };
            }
            return new()
            {
                Message = "An Error Ocurred!"
            };
        }
    }
}
