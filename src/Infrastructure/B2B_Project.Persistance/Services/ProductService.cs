using B2B_Project.Application;
using B2B_Project.Application.DTOs.Product;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace B2B_Project.Persistance.Services
{
    public class ProductService : IProductService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly IImageWriteRepository _imageWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        public ProductService(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository, ICompanyReadRepository companyReadRepository, UserManager<AppUser> userManager, IProductWriteRepository productWriteRepository, IImageWriteRepository imageWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _companyReadRepository = companyReadRepository;
            _userManager = userManager;
            _productWriteRepository = productWriteRepository;
            _imageWriteRepository = imageWriteRepository;
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
                .Where(x => x.CompanyId == companyID && x.DeletedDate==null)
                .OrderBy(x=>x.CreatedDate)
                .ToListAsync();

            return products;
        }
        public async Task<bool> CreateProductAsync(CreateProductDto model)
        {
            Product p = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ProductCode = model.ProductCode,
                CategoryId = model.CategoryId,
                CompanyId = model.CompanyId
            };

            await _productWriteRepository.AddAsync(p);
            int res = await _productWriteRepository.SaveAsync();

            if (res > 0)
            {
                if (model.ProductImages != null && model.ProductImages.Any())
                {
                    foreach (var item in model.ProductImages)
                    {
                        if (!string.IsNullOrEmpty(item.FileName))
                        {
                            string directory = Path.Combine("wwwroot", "images", p.GetType().Name);
                            //{DateTime.UtcNow:yyyyMMdd}
                            string fileName = $"{p.Id}_{item.FileName}";
                            string filePath = Path.Combine(directory, fileName);

                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }

                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                            }

                            Image img = new()
                            {
                                ImageName = item.FileName,
                                ImageUrl = filePath,
                                EntityType = p.GetType().Name,
                                EntityId = p.Id.ToString()
                            };
                            await _imageWriteRepository.AddAsync(img);
                        }
                    }

                    await _imageWriteRepository.SaveAsync();
                }

                return true;
            }

            return false;
        }

    }
}
