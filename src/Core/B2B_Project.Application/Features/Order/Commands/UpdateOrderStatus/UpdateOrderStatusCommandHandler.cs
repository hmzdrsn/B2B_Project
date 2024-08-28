using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Order.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommandRequest, HandlerResponse<UpdateOrderStatusCommandResponse>>
    {
        private readonly IOrderService _orderService;

        public UpdateOrderStatusCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<HandlerResponse<UpdateOrderStatusCommandResponse>> Handle(UpdateOrderStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var res = await _orderService.UpdateOrderStatusAsync(request);
            if (res)
                return new()
                {
                    Message = "Order Updated"
                };
            return new()
            {
                Message = "Order Update Error!"
            };
        }
    }
}
