using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.ProductDiscount.Commands.RemoveProductDiscountById
{
    public class RemoveProductDiscountByIdCommandHandler : IRequestHandler<RemoveProductDiscountByIdCommandRequest, HandlerResponse<RemoveProductDiscountByIdCommandResponse>>
    {
        private readonly IProductDiscountWriteRepository _productDiscountWrite;

        public RemoveProductDiscountByIdCommandHandler(IProductDiscountWriteRepository productDiscountWrite)
        {
            _productDiscountWrite = productDiscountWrite;
        }

        public async Task<HandlerResponse<RemoveProductDiscountByIdCommandResponse>> Handle(RemoveProductDiscountByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var resCount = await _productDiscountWrite.Table
                .Where(x => x.Id.ToString() == request.ProductDiscountId)
                .ExecuteUpdateAsync(update => update.SetProperty(x =>
                x.DeletedDate, DateTime.Now));
            if (resCount > 0)
            {
                return new()
                {
                    Status = "true"
                };
            }
            return new()
            {
                Status = "An error ocurred removing product discount!"
            };
        }
    }
}
