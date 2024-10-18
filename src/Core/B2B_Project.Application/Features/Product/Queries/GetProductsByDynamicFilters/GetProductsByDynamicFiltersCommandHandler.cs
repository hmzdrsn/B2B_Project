using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByDynamicFilters
{
    public class GetProductsByDynamicFiltersCommandHandler : IRequestHandler<GetProductsByDynamicFiltersCommandRequest, HandlerResponse<List<GetProductsByDynamicFiltersCommandResponse>>>
    {
        private readonly IProductReadRepository _productRead;

        public GetProductsByDynamicFiltersCommandHandler(IProductReadRepository productRead)
        {
            _productRead = productRead;
        }

        public async Task<HandlerResponse<List<GetProductsByDynamicFiltersCommandResponse>>> Handle(GetProductsByDynamicFiltersCommandRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Domain.Entities.Product> query = _productRead.GetAll();
            //if (!string.IsNullOrEmpty(request.CategoryId))
            //{
            //    query = query.Where(x => x.CategoryId.ToString() == request.CategoryId);
            //}
            //if (request.Price.HasValue)
            //{
            //    query = query.Where(x => x.Price > request.Price);
            //}
            //if (!string.IsNullOrEmpty(request.CompanyId))
            //{
            //    query = query.Where(x => x.CompanyId.ToString() == request.CompanyId);
            //}
            //query = query.Where(x => x.DeletedDate == null);

            //var products = await query
            //    .Select(x => new GetProductsByDynamicFiltersCommandResponse()
            //    {
            //        Name = x.Name,
            //        Description = x.Description,
            //        Price = x.Price,
            //        Stock = x.Stock
            //    })
            //    .ToListAsync(cancellationToken);
            //return new()
            //{
            //    Data = products ?? new()
            //};
            var products = await _productRead
                .WhereDynamic("stock", 10, "<")
                .Select(x => new GetProductsByDynamicFiltersCommandResponse()
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock
                })
                .ToListAsync();
            return new()
            {
                Data = products ?? new()
            };
        }
    }
}
