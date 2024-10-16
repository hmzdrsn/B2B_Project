using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.UserDiscount.Commands.AssignDiscountToUser
{
    public class AssignDiscountToUserQueryHandler : IRequestHandler<AssignDiscountToUserQueryRequest, HandlerResponse<AssignDiscountToUserQueryResponse>>
    {
        private readonly IDiscountReadRepository _discountRead;
        private readonly IUserDiscountWriteRepository _userDiscountWrite;
        private readonly IUserDiscountReadRepository _userDiscountRead;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyReadRepository _companyRead;

        public AssignDiscountToUserQueryHandler(IUserDiscountWriteRepository userDiscountWrite, UserManager<AppUser> userManager, IDiscountReadRepository discountRead, ICompanyReadRepository companyRead, IUserDiscountReadRepository userDiscountRead)
        {
            _userDiscountWrite = userDiscountWrite;
            _userManager = userManager;
            _discountRead = discountRead;
            _companyRead = companyRead;
            _userDiscountRead = userDiscountRead;
        }
        public async Task<HandlerResponse<AssignDiscountToUserQueryResponse>> Handle(AssignDiscountToUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userCompany = await _userManager.FindByNameAsync(request.Username);
            var userTo = await _userManager.FindByNameAsync(request.UsernameTo);
            if (userCompany == null || userTo == null)
            {
                return new()
                {
                    Message = "User Not Found!"
                };
            }
            var company = await _companyRead
                .Table
                .Where(x => x.PrimaryAppUserID == userCompany.Id
                || x.SecondaryAppUserID == userCompany.Id)
                .FirstOrDefaultAsync();
            if (company == null)
            {
                return new()
                {
                    Message = "Company Not Found!"
                };
            }
            //gelen discountid giriþ yapan þirkete mi ait
            var companyHasDiscount = await _discountRead
                .Table
                .AnyAsync(x => x.Id.ToString() == request.DiscountId
                && x.CompanyId == company.Id
                && x.DeletedDate == null);
            var userAlreadyHaveDiscount = await _userDiscountRead
                .Table
                .AnyAsync(x => x.AppUserId == userTo.Id
                && x.DiscountId.ToString() == request.DiscountId
                && x.DeletedDate == null);
            if (!companyHasDiscount)
            {
                return new()
                {
                    Message = "The company has no discount"
                };
            }
            if (userAlreadyHaveDiscount)
            {
                return new()
                {
                    Message = "User already have the discount!"
                };
            }

            await _userDiscountWrite.AddAsync(new()
            {
                UsageCount = 0,
                AppUserId = userTo.Id,
                DiscountId = Guid.Parse(request.DiscountId),
            });
            var resCount = await _userDiscountWrite.SaveAsync();
            if (resCount > 0)
            {
                return new()
                {
                    Status = "true",
                    Message = "The discount is assigned to user succesfully!"
                };
            }
            return new()
            {
                Status = "false",
                Message = "An error ocurred assigning the discount!"
            };
        }
    }
}
