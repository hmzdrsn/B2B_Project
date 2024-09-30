using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Message.Commands.SendMessage
{
    public class SendMessageCommandRequest : IRequest<HandlerResponse<SendMessageCommandResponse>>
    {
        public string senderUsername { get; set; } = default!;
        public string receiverId { get; set; } = default!;
        public string content { get; set; } = default!;
    }
}
