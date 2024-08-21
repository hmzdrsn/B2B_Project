using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, HandlerResponse<UpdateProductCommandResponse>>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<UpdateProductCommandResponse>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _productService.UpdateProductAsync(request))
            {
                return new()
                {
                    Message = "Product Updated Successfully!"
                };
            }
            return new()
            {
                Message = "Product Update Error!"
            };
        }
    }

}
