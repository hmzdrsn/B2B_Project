using B2B_Project.Application.Common.Models;
using B2B_Project.Application.DTOs.Product;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQueryRequest, HandlerResponse<GetProductsByCategoryQueryResponse>>
    {
        private readonly IProductService _productService;

        public GetProductsByCategoryQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<GetProductsByCategoryQueryResponse>> Handle(GetProductsByCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            //var data = await _productReadRepository
            //    .GetAll()
            //    .Include(x => x.Category)
            //    .Where(x => x.CategoryId == request.CategoryId)
            //    .Select(x=>new DTOs.Product.GetProductsByCategory()
            //    {
            //        Name = x.Name,
            //        Description = x.Description,
            //        Price = x.Price,
            //        Stock  =x.Stock
            //    })
            //    .ToListAsync();
            var data = await _productService.GetProductsByCategoryAsync(request.CategoryId);

            var filteredProducts = data.Select(x => new DTOs.Product.GetProductsByCategoryDto()
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock
            }).ToList();

            GetProductsByCategoryQueryResponse response = new()
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
