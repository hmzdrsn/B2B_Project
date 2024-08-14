using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<HandlerResponse<CreateOrderCommandResponse>>
    {
        public string? Address { get; set; }
        public string? Username { get; set; }
    }

}
