using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, HandlerResponse<GetByIdProductQueryResponse>>
    {
        private readonly IProductService _productService;

        public GetByIdProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<GetByIdProductQueryResponse>> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            GetByIdProductQueryResponse res = await _productService.GetProductByIdWithImageAndCategory(request);

            return new()
            {
                Data = res,
            };
        }
    }

}
