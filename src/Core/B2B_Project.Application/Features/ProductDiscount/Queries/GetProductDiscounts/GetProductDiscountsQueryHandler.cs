
using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscounts
{
    public class GetProductDiscountsQueryHandler : IRequestHandler<GetProductDiscountsQueryRequest, HandlerResponse<List<GetProductDiscountsQueryResponse>>>
    {
        private readonly IProductDiscountReadRepository _productDiscountRead;
        private readonly IDiscountReadRepository _discountRead;
        private readonly UserManager<AppUser> _userManager;
        public GetProductDiscountsQueryHandler(IProductDiscountReadRepository productDiscountRead, IDiscountReadRepository discountRead, UserManager<AppUser> userManager)
        {
            _productDiscountRead = productDiscountRead;
            _discountRead = discountRead;
            _userManager = userManager;
        }
        public async Task<HandlerResponse<List<GetProductDiscountsQueryResponse>>> Handle(GetProductDiscountsQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new()
                {
                    Message = "User Not Found!"
                };
            }
            var data = await _productDiscountRead
                .Table
                .Where(x =>
                 (x.Discount.Company.PrimaryAppUserID == user.Id
                 || x.Discount.Company.SecondaryAppUserID == user.Id)
                 && x.DeletedDate == null)
                .Select(x => new GetProductDiscountsQueryResponse()
                {
                    ProductDiscountId = x.Id.ToString(),
                    DiscountCode = x.Discount.DiscountCode,
                    ProductName = x.Product.Name,
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
