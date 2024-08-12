using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQueryRequest : IRequest<HandlerResponse< GetProductsByCategoryQueryResponse>>
    {
        public Guid CategoryId { get; set; }
    }
}
