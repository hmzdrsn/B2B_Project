using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsCount
{
    public class GetProductsCountQueryHandler : IRequestHandler<GetProductsCountQueryRequest, HandlerResponse<GetProductsCountQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductsCountQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<HandlerResponse<GetProductsCountQueryResponse>> Handle(GetProductsCountQueryRequest request, CancellationToken cancellationToken)
        {
            int count = await _productReadRepository.GetAll().Where(x => x.DeletedDate == null).CountAsync(cancellationToken);
            GetProductsCountQueryResponse res = new()
            {
                ProductsCount = count
            };

            return new()
            {
                Data = res
            };
        }
    }

}
