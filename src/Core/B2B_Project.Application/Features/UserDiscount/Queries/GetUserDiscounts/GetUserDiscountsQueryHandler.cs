
using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.UserDiscount.Queries.GetUserDiscounts
{
    public class GetUserDiscountsQueryHandler : IRequestHandler<GetUserDiscountsQueryRequest, HandlerResponse<List<GetUserDiscountsQueryResponse>>>
    {
        private readonly IUserDiscountReadRepository _userDiscountRead;
        private readonly IDiscountReadRepository _discountRead;
        private readonly UserManager<AppUser> _userManager;

        public GetUserDiscountsQueryHandler(IUserDiscountReadRepository userDiscountRead, IDiscountReadRepository discountRead, UserManager<AppUser> userManager)
        {
            _userDiscountRead = userDiscountRead;
            _discountRead = discountRead;
            _userManager = userManager;
        }

        public async Task<HandlerResponse<List<GetUserDiscountsQueryResponse>>> Handle(GetUserDiscountsQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new()
                {
                    Message = "User Not Found!"
                };
            }
            var data = await _userDiscountRead
                .Table
                .Where(x =>
                 (x.Discount.Company.PrimaryAppUserID == user.Id
                 || x.Discount.Company.SecondaryAppUserID == user.Id)
                 && x.DeletedDate == null)
                .Select(x => new GetUserDiscountsQueryResponse()
                {
                    UserDiscountId = x.Id.ToString(),
                    DiscountCode = x.Discount.DiscountCode,
                    Username = x.User.UserName
                })
                .ToListAsync();
            if (data != null)
            {
                return new()
                {
                    Data = data
                };
            }
            return new()
            {
                Data = new()
            };
        }
    }
}
