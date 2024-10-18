
using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByDynamicFilters
{
    public class GetProductsByDynamicFiltersCommandRequest : IRequest<HandlerResponse<List<GetProductsByDynamicFiltersCommandResponse>>>
    {
        public decimal? Price { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
    }
}
