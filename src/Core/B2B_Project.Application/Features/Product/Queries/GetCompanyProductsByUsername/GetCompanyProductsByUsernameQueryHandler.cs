using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Product.Queries.GetCompanyProductsByUsername
{
    public class GetCompanyProductsByUsernameQueryHandler :
        IRequestHandler<GetCompanyProductsByUsernameQueryRequest, HandlerResponse<GetCompanyProductsByUsernameQueryResponse>>
    {
        private readonly IProductService _productService;

        public GetCompanyProductsByUsernameQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<GetCompanyProductsByUsernameQueryResponse>> Handle(GetCompanyProductsByUsernameQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetCompanyProductsByUsername(request.Username);
            var filteredProducts = products.Select(x => new DTOs.Product.GetCompanyProductsByUsername()
            {
                Name = x.Name,
                Description = x.Description,
                Price  =x.Price,
                Stock = x.Stock
            }).ToList();
            GetCompanyProductsByUsernameQueryResponse response = new()
            {
                Products = filteredProducts
            };

            return new()
            {
                Data = response
            };
        }
    }
}
