using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.User.Queries
{
    public class GetAllUserQueryRequest : IRequest<HandlerResponse<GetAllUserQueryResponse>>
    {
    }
}
