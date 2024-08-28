using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Order.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandRequest : IRequest<HandlerResponse<UpdateOrderStatusCommandResponse>>
    {
        public string OrderId { get; set; }
        public string OrderStatusId { get; set; }
    }
}
