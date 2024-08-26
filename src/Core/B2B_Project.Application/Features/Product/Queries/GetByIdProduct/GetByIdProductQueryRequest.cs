using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetByIdProduct
{
    public class GetByIdProductQueryRequest: IRequest<HandlerResponse<GetByIdProductQueryResponse>>
    {
        public string ProductId { get; set; }
    }

}
