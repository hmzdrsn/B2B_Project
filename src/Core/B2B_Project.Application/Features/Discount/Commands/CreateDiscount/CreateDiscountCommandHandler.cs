using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Discount.Commands.CreateDiscount
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommandRequest, HandlerResponse<CreateDiscountCommandResponse>>
    {
        private readonly IDiscountWriteRepository _discountWriteRepository;
        private readonly IDiscountReadRepository _discountReadRepository;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly UserManager<AppUser> _userManager;
        public CreateDiscountCommandHandler(IDiscountWriteRepository discountWriteRepository, IDiscountReadRepository discountReadRepository, ICompanyReadRepository companyReadRepository, UserManager<AppUser> userManager)
        {
            _discountWriteRepository = discountWriteRepository;
            _discountReadRepository = discountReadRepository;
            _companyReadRepository = companyReadRepository;
            _userManager = userManager;
        }

        public async Task<HandlerResponse<CreateDiscountCommandResponse>> Handle(CreateDiscountCommandRequest request, CancellationToken cancellationToken)
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
            //if (request.DiscountAmount <= 0 || request.DiscountAmount > 100)
            //{
            //    return new()
            //    {
            //        Message = "Check the Discount Rate!"
            //    };
            //}
            if (request.MaxUsagePerUser < 0)
            {
                return new()
                {
                    Message = "Check the Max Usage Per User!"
                };
            }
            TimeSpan x = TimeSpan.FromMinutes(5);
            if (request.ValidFrom > request.ValidUntil || request.ValidFrom < DateTime.Now - x)
            {
                return new()
                {
                    Message = "Check the Valid Dates!"
                };
            }
            var existingDiscount = await _discountReadRepository.Table
                .Where(x => x.DiscountCode == request.DiscountCode && x.DeletedDate == null)
                .FirstOrDefaultAsync(cancellationToken);
            if (existingDiscount != null)
            {
                return new()
                {
                    Message = "Discount with the same code already exists!"
                };
            }

            Domain.Entities.Discount discount = new()
            {
                DiscountAmount = request.DiscountAmount,
                isPercentage = request.isPercentage,
                DiscountCode = request.DiscountCode,
                ValidFrom = request.ValidFrom,
                ValidUntil = request.ValidUntil,
                MaxUsagePerUser = request.MaxUsagePerUser,
                CompanyId = company.Id,
            };

            await _discountWriteRepository.AddAsync(discount);
            var saveCount = await _discountWriteRepository.SaveAsync();
            if (saveCount > 0)
            {
                return new() { Message = "Discount Created Successfully", Status = "true" };
            }
            return new()
            {
                Message = "An error ocurred creating Discount"
            };
        }
    }

}
