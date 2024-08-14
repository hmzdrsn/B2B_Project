using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using MediatR;

namespace B2B_Project.Application.Features.Order.Queries
{
    public class GetOrdersByCompanyQueryHandler : IRequestHandler<GetOrdersByCompanyQueryRequest, HandlerResponse<GetOrdersByCompanyQueryResponse>>
    {
        private readonly IOrderService _orderService;

        public GetOrdersByCompanyQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<HandlerResponse<GetOrdersByCompanyQueryResponse>> Handle(GetOrdersByCompanyQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _orderService.GetOrdersByCompany(request.Username);
            if (data!=null)
            {
                GetOrdersByCompanyQueryResponse response = new()
                {
                    Orders = data
                };
                return new()
                {
                    Data = response
                };
            }
            return new()
            {
                Message = "No Order"
            };

        }
    }

}
