using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<HandlerResponse<CreateOrderCommandResponse>>
    {
        public string? Username { get; set; }
        public string? OrderStatusId { get; set; }
    }

}
