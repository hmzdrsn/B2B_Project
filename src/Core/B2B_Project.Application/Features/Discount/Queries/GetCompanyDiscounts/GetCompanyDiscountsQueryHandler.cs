
using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Discount.Queries.GetCompanyDiscounts
{
    public class GetCompanyDiscountsQueryHandler : IRequestHandler<GetCompanyDiscountsQueryRequest, HandlerResponse<List<GetCompanyDiscountsQueryResponse>>>
    {
        private readonly IDiscountReadRepository _discountReadRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyReadRepository _companyReadRepository;

        public GetCompanyDiscountsQueryHandler(IDiscountReadRepository discountReadRepository, UserManager<AppUser> userManager, ICompanyReadRepository companyReadRepository)
        {
            _discountReadRepository = discountReadRepository;
            _userManager = userManager;
            _companyReadRepository = companyReadRepository;
        }
        public async Task<HandlerResponse<List<GetCompanyDiscountsQueryResponse>>> Handle(GetCompanyDiscountsQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new()
                {
                    Message = "User Not Found!"
                };
            }
            var company = await _companyReadRepository.Table
                .Where(x => x.PrimaryAppUserID == user.Id || x.SecondaryAppUserID == user.Id)
                .FirstOrDefaultAsync();
            if (company == null)
            {
                return new()
                {
                    Message = "Company Not Found!"
                };
            }
            var companyDiscounts = await _discountReadRepository.Table
                .Where(x => x.CompanyId == company.Id && x.DeletedDate == null)
                .Select(x => new GetCompanyDiscountsQueryResponse()
                {
                    DiscountId = x.Id.ToString(),
                    DiscountCode = x.DiscountCode,
                    DiscountAmount = x.DiscountAmount,
                    isPercentage = x.isPercentage,
                    MaxUsagePerUser = x.MaxUsagePerUser,
                    ValidFrom = x.ValidFrom,
                    ValidUntil = x.ValidUntil,
                    CompanyId = x.CompanyId
                })
                .ToListAsync();

            if (companyDiscounts != null)
            {
                return new()
                {
                    Data = companyDiscounts
                };
            }
            return new()
            {
                Message = "Discount Not found!"
            };
        }
    }

}
