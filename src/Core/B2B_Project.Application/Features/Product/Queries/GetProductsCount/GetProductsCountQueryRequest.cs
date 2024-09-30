using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsCount
{
    public class GetProductsCountQueryRequest : IRequest<HandlerResponse<GetProductsCountQueryResponse>>
    {
    }

}
