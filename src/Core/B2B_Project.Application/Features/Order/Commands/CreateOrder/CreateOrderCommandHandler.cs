using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, HandlerResponse<CreateOrderCommandResponse>>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<HandlerResponse<CreateOrderCommandResponse>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _orderService.CreateOrderAsync(new() { Username = request.Username, OrderStatusId = request.OrderStatusId }))
            {
                return new()
                {
                    Message = "Order Created"
                };
            }
            return new()
            {
                Message = "An error occurred while placing the order"
            };
        }

    }

}
