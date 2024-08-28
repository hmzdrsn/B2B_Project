using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.OrderStatus.Queries.GetAll
{
    public class GetAllOrderStatusQueryHandler : IRequestHandler<GetAllOrderStatusQueryRequest, HandlerResponse<GetAllOrderStatusQueryResponse>>
    {
        private readonly IOrderStatusReadRepository _orderStatusReadRepository;

        public GetAllOrderStatusQueryHandler(IOrderStatusReadRepository orderStatusReadRepository)
        {
            _orderStatusReadRepository = orderStatusReadRepository;
        }

        public async Task<HandlerResponse<GetAllOrderStatusQueryResponse>> Handle(GetAllOrderStatusQueryRequest request, CancellationToken cancellationToken)
        {
            var orderStatuses = await _orderStatusReadRepository.GetAll()
                .Select(x => new StatusResponse()
                {
                    Id = x.Id.ToString(),
                    Status = x.Status,
                })
                .ToListAsync();
            if(orderStatuses !=null)
            {
                GetAllOrderStatusQueryResponse res = new()
                {
                    Status = orderStatuses
                };

                return new()
                {
                    Data = res
                };
            }
            return new()
            {
                Message = "No Order Statuses!"
            };
        }
    }
}
