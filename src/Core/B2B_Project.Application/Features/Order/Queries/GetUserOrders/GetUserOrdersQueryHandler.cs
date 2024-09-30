using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Order.Queries.GetUserOrders
{

    public class GetUserOrdersHandler : IRequestHandler<GetUserOrdersQueryRequest, HandlerResponse<List<GetUserOrdersQueryResponse>>>
    {
        private readonly IOrderService _orderService;

        public GetUserOrdersHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<HandlerResponse<List<GetUserOrdersQueryResponse>>> Handle(GetUserOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _orderService.GetUserOrders(request);
            if (res == null)
            {
                return new()
                {
                    Message = "An Error Ocurred Retrieving Orders"
                };
            }
            return new()
            {
                Data = res
            };
        }
    }
}
