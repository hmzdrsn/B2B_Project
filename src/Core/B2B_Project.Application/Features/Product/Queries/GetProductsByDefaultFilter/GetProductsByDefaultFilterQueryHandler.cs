using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetDefaultProductsByFilter
{
    public class GetProductsByDefaultFilterQueryHandler : IRequestHandler<GetProductsByDefaultFilterQueryRequest, HandlerResponse<List<GetProductsByDefaultFilterQueryResponse>>>
    {
        private readonly IProductService _productService;

        public GetProductsByDefaultFilterQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<List<GetProductsByDefaultFilterQueryResponse>>> Handle(GetProductsByDefaultFilterQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _productService.GetProductsByDefaultFilter(request);
            if(res != null)
            {
                return new()
                {
                    Data = res
                };
            }
            return new()
            {
                Message = "Get Product by Filter Error! Check Your Filter"
            };
        }
    }
}
