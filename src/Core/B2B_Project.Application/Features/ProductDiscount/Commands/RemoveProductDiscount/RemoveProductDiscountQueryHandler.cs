
using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.ProductDiscount.Commands.RemoveProductDiscount
{
    public class RemoveProductDiscountQueryHandler : IRequestHandler<RemoveProductDiscountQueryRequest, HandlerResponse<RemoveProductDiscountQueryResponse>>
    {
        private readonly IProductDiscountWriteRepository _productDiscountWrite;

        public RemoveProductDiscountQueryHandler(IProductDiscountWriteRepository productDiscountWrite)
        {
            _productDiscountWrite = productDiscountWrite;
        }

        public async Task<HandlerResponse<RemoveProductDiscountQueryResponse>> Handle(RemoveProductDiscountQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _productDiscountWrite
                .Table
                .Where(x => x.ProductId.ToString() == request.ProductId
                && x.DiscountId.ToString() == request.DiscountId)
                .ExecuteUpdateAsync(update => update.SetProperty(x =>
                x.DeletedDate, DateTime.Now));
            if (res > 0)
            {
                return new()
                {
                    Status = "true"
                };
            }
            return new()
            {
                Status = "An Error Ocurred Removing Product Discount!"
            };
        }
    }
}
