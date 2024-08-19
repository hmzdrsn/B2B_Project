using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Category.Queries.GetAll
{
    public class GetAllCategoryQueryRequest : IRequest<HandlerResponse<GetAllCategoryQueryResponse>>
    {
    }

}
