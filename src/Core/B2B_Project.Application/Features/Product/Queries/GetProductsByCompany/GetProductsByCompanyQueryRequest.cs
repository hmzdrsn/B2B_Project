using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByCompany
{
    public class GetProductsByCompanyQueryRequest : IRequest<HandlerResponse<GetProductsByCompanyQueryResponse>>
    {
        public Guid CompanyId { get; set; }
    }
}
