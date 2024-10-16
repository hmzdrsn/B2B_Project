
using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.ProductDiscount.Commands.AssignDiscountToProduct
{
    public class AssignDiscountToProductCommandHandler : IRequestHandler<AssignDiscountToProductCommandRequest, HandlerResponse<AssignDiscountToProductCommandResponse>>
    {
        private readonly IProductDiscountWriteRepository _productDiscountWrite;
        private readonly IProductDiscountReadRepository _productDiscountRead;
        private readonly ICompanyReadRepository _companyRead;
        private readonly UserManager<AppUser> _userManager;

        public AssignDiscountToProductCommandHandler(IProductDiscountWriteRepository productDiscountWrite, IProductDiscountReadRepository productDiscountRead, UserManager<AppUser> userManager, ICompanyReadRepository companyRead)
        {
            _productDiscountWrite = productDiscountWrite;
            _productDiscountRead = productDiscountRead;
            _userManager = userManager;
            _companyRead = companyRead;
        }

        public async Task<HandlerResponse<AssignDiscountToProductCommandResponse>> Handle(AssignDiscountToProductCommandRequest request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new()
                {
                    Message = "User Not Found!"
                };
            }
            var companyHasProduct = await _companyRead.Table
                .Where(c => (c.PrimaryAppUserID == user.Id || c.SecondaryAppUserID == user.Id)
                && c.Products.Any(p => p.Id.ToString() == request.ProductId))
                .AnyAsync();

            if (!companyHasProduct)
            {
                return new HandlerResponse<AssignDiscountToProductCommandResponse>
                {
                    Message = "Product does not belong to the company!",
                };
            }

            Guid discountId = Guid.Parse(request.DiscountId);
            bool existDiscount = await _productDiscountRead.Table
                .AnyAsync(x => x.DiscountId == discountId && x.ProductId.ToString() == request.ProductId && x.DeletedDate == null);
            if (existDiscount)
            {
                return new()
                {
                    Message = "Existing Discount"
                };
            }
            await _productDiscountWrite.AddAsync(new()
            {
                DiscountId = discountId,
                ProductId = Guid.Parse(request.ProductId),
            });
            var resCount = await _productDiscountWrite.SaveAsync();
            if (resCount > 0)
            {
                return new()
                {
                    Message = "Discount Assigned",
                    Status = "true"
                };
            }
            return new()
            {
                Message = "An Error Ocurred Assigning Discount!"
            };
        }
    }
}
