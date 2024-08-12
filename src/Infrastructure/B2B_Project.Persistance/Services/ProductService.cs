using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class ProductService : IProductService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly UserManager<AppUser> _userManager;
        public ProductService(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository, ICompanyReadRepository companyReadRepository, UserManager<AppUser> userManager)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _companyReadRepository = companyReadRepository;
            _userManager = userManager;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var ids = await GetCategoryIds(categoryId);
            var products = await _productReadRepository.Table
                .Where(p => p.CategoryId.HasValue && ids.Contains(p.CategoryId.Value))
                .ToListAsync();

            return products;
        }


        private async Task<List<Guid>> GetCategoryIds(Guid id)
        {
            var idList = new List<Guid>();
            var stack = new Stack<Guid>();

            stack.Push(id);

            while (stack.Count > 0)
            {
                var currentId = stack.Pop();
                if (!idList.Contains(currentId))
                {
                    idList.Add(currentId);
                    var childCategories = await _categoryReadRepository.Table.Where(x => x.ParentCategoryId == currentId).Select(x => x.Id).ToListAsync();
                    foreach (var childId in childCategories)
                    {
                        stack.Push(childId);
                    }
                }
            }

            return idList;
        }

       
        public async Task<List<Product>> GetCompanyProductsByUsername(string userName)
        {
            var user =  await _userManager.FindByNameAsync(userName);

            Guid companyID = await _companyReadRepository.Table
                .Where(x => x.PrimaryAppUserID == user.Id || x.SecondaryAppUserID == user.Id)
                .Select(x=>x.Id)
                .FirstOrDefaultAsync();

            var products = await _productReadRepository.Table
                .Where(x => x.CompanyId == companyID)
                .ToListAsync();

            return products;
        }
    }
}
