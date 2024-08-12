using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Product.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, HandlerResponse<GetAllProductQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<HandlerResponse<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _productReadRepository.GetAll()
                .Select(product => new DTOs.Product.GetAllProduct()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock
                }).ToListAsync();

            GetAllProductQueryResponse response = new()
            {
                Products = data,
            };
            return new()
            {
                Data = response,
                Status = "400",
            };
        }
    }
}
