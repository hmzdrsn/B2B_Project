using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Order.Queries.GetOrderWithDetailsById
{
    public class GetOrderWithDetailsByIdQueryHandler : IRequestHandler<GetOrderWithDetailsByIdQueryRequest, HandlerResponse<GetOrderWithDetailsByIdQueryResponse>>
    {
        private readonly IOrderService _orderService;

        public GetOrderWithDetailsByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<HandlerResponse<GetOrderWithDetailsByIdQueryResponse>> Handle(GetOrderWithDetailsByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _orderService.GetOrderWithDetailsById(request);

            if(res !=null)
            {
                return new()
                {
                    Data = res
                };
            }
            return new()
            {
                Message = "Order could not be delivered!"
            };
        }
    }
}
