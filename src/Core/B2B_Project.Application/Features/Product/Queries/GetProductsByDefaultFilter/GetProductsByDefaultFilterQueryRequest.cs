using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetDefaultProductsByFilter
{
    public class GetProductsByDefaultFilterQueryRequest : IRequest<HandlerResponse<List<GetProductsByDefaultFilterQueryResponse>>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
