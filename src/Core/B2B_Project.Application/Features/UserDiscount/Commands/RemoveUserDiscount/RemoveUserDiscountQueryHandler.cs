using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.UserDiscount.Commands.RemoveUserDiscount
{
    public class RemoveUserDiscountQueryHandler : IRequestHandler<RemoveUserDiscountQueryRequest, HandlerResponse<RemoveUserDiscountQueryResponse>>
    {
        private readonly IUserDiscountWriteRepository _userDiscountWriteRepository;
        public RemoveUserDiscountQueryHandler(IUserDiscountWriteRepository userDiscountWriteRepository)
        {
            _userDiscountWriteRepository = userDiscountWriteRepository;
        }

        public async Task<HandlerResponse<RemoveUserDiscountQueryResponse>> Handle(RemoveUserDiscountQueryRequest request, CancellationToken cancellationToken)
        {
            var resCount = await _userDiscountWriteRepository.Table
                .Where(x => x.Id.ToString() == request.UserDiscountId)
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
                Status = "An error ocurred removing user discount!"
            };
        }
    }
}
