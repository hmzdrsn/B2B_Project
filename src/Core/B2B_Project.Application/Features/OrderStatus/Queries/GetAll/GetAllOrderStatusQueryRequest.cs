using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.OrderStatus.Queries.GetAll
{
    public class GetAllOrderStatusQueryRequest: IRequest<HandlerResponse<GetAllOrderStatusQueryResponse>>
    {
    }
}
