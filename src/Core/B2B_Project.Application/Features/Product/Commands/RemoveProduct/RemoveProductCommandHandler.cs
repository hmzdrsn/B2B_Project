using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Product.Commands.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, HandlerResponse<RemoveProductCommandResponse>>
    {
        private readonly IProductService _productService;

        public RemoveProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<RemoveProductCommandResponse>> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _productService.DeleteById(Guid.Parse(request.ProductId)))
            {
                return new()
                {
                    Message = "Product DeletedDate Updated"
                };
            }
            return new()
            {
                Message = "An error occurred while deleting!"
            };
        }
    }

}
