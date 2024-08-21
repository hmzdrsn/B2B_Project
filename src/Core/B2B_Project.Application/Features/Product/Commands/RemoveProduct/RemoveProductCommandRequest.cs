using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Product.Commands.RemoveProduct
{
    public class RemoveProductCommandRequest : IRequest<HandlerResponse<RemoveProductCommandResponse>>
    {
        public string ProductId { get; set; }
    }

}
