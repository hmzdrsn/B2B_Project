using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Discount.Commands.RemoveDiscount
{
    public class RemoveDiscountCommandHandler : IRequestHandler<RemoveDiscountCommandRequest, HandlerResponse<RemoveDiscountCommandResponse>>
    {
        private readonly IDiscountReadRepository _discountReadRepository;
        private readonly IDiscountWriteRepository _discountWriteRepository;
        private readonly IProductDiscountWriteRepository _productDiscountWrite;
        private readonly IUserDiscountWriteRepository _userDiscountWrite;


        public RemoveDiscountCommandHandler(IDiscountReadRepository discountReadRepository, IDiscountWriteRepository discountWriteRepository, IProductDiscountWriteRepository productDiscountWrite, IUserDiscountWriteRepository userDiscountWrite)
        {
            _discountReadRepository = discountReadRepository;
            _discountWriteRepository = discountWriteRepository;
            _productDiscountWrite = productDiscountWrite;
            _userDiscountWrite = userDiscountWrite;
        }

        public async Task<HandlerResponse<RemoveDiscountCommandResponse>> Handle(RemoveDiscountCommandRequest request, CancellationToken cancellationToken)
        {
            var discont = await _discountReadRepository.GetByIdAsync(request.DiscountId);
            if (discont != null)
            {
                _discountWriteRepository.Remove(discont);
                var resCount = await _discountWriteRepository.SaveAsync();
                if (resCount > 0)
                {
                    await _productDiscountWrite.Table
                        .Where(x => x.DiscountId == discont.Id)
                        .ExecuteUpdateAsync(update => update.SetProperty(x => x.DeletedDate, DateTime.Now));
                    await _userDiscountWrite.Table
                        .Where(x => x.DiscountId == discont.Id)
                        .ExecuteUpdateAsync(update => update.SetProperty(x => x.DeletedDate, DateTime.Now));
                    return new()
                    {
                        Message = "Discount Removed",
                        Status = "true"
                    };
                }
            }
            return new()
            {
                Message = "An Error Ocurred Removing Discount!"
            };
        }
    }

}
