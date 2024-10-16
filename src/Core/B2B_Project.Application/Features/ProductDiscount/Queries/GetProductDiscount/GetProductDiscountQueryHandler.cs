
using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscount
{
    public class GetProductDiscountQueryHandler : IRequestHandler<GetProductDiscountQueryRequest, HandlerResponse<List<GetProductDiscountQueryResponse>>>
    {
        private readonly IDiscountReadRepository _discountRead;
        private readonly IProductDiscountReadRepository _productDiscountRead;

        public GetProductDiscountQueryHandler(IDiscountReadRepository discountRead, IProductDiscountReadRepository productDiscountRead)
        {
            _discountRead = discountRead;
            _productDiscountRead = productDiscountRead;
        }

        public async Task<HandlerResponse<List<GetProductDiscountQueryResponse>>> Handle(GetProductDiscountQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _productDiscountRead.Table
                .Where(x => x.ProductId.ToString() == request.ProductId && x.DeletedDate == null && x.Discount.DeletedDate == null)
                //.Include(x => x.Discount)  gerek kalmadan getiriyor.
                .Select(x => new GetProductDiscountQueryResponse()
                {
                    DiscountId = x.DiscountId.ToString(),
                    DiscountAmount = x.Discount.DiscountAmount,
                    DiscountCode = x.Discount.DiscountCode,
                    isPercentage = x.Discount.isPercentage
                })
                .ToListAsync();
            if (res != null)
            {
                return new()
                {
                    Data = res
                };
            }
            //TAMAMLANACAK
            return new()
            {
                Data = new()
            };
        }
    }
}
