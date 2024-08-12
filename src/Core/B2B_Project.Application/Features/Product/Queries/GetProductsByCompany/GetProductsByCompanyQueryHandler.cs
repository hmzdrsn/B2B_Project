using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByCompany
{
    public class GetProductsByCompanyQueryHandler : IRequestHandler<GetProductsByCompanyQueryRequest, HandlerResponse<GetProductsByCompanyQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductsByCompanyQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<HandlerResponse<GetProductsByCompanyQueryResponse>> Handle(GetProductsByCompanyQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _productReadRepository.GetProductsByCompany(request.CompanyId)
                .Select(x=> new DTOs.Product.GetProductsByCompany()
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock
                }).ToListAsync();

            GetProductsByCompanyQueryResponse response = new()
            {
                Products = data
            };

            return new()
            {
                Data = response,
            };
        }
    }
}
