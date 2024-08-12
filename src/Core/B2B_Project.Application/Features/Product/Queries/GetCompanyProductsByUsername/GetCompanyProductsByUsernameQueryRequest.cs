using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetCompanyProductsByUsername
{
    public class GetCompanyProductsByUsernameQueryRequest : IRequest<HandlerResponse<GetCompanyProductsByUsernameQueryResponse>>
    {
        public string Username { get; set; }
    }
}
