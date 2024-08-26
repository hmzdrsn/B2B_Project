using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Image.Commands.RemoveImage
{
    public class RemoveImageCommandRequest : IRequest<HandlerResponse<RemoveImageCommandResponse>>
    {
        public Guid ImageId { get; set; }
    }
}
