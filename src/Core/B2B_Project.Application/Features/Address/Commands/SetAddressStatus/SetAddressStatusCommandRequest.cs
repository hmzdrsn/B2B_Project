using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Address.Commands.SetAddressStatus
{
    public class SetAddressStatusCommandRequest : IRequest<HandlerResponse<SetAddressStatusCommandResponse>>
    {
        public string AddressId { get; set; } = default!;
        public string Username { get; set; } = default!;
    }
}
