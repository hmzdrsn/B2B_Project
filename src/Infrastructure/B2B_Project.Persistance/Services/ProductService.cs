using B2B_Project.Application;
using B2B_Project.Application.DTOs.Product;
using B2B_Project.Application.Features.Product.Commands.UpdateProduct;
using B2B_Project.Application.Features.Product.Queries.GetByIdProduct;
using B2B_Project.Application.Features.Product.Queries.GetDefaultProductsByFilter;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class ProductService : IProductService
    {
        #region dependencies

        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly IImageWriteRepository _imageWriteRepository;
        private readonly IImageReadRepository _imageReadRepository;
        private readonly UserManager<AppUser> _userManager;
        public ProductService(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository, ICompanyReadRepository companyReadRepository, UserManager<AppUser> userManager, IProductWriteRepository productWriteRepository, IImageWriteRepository imageWriteRepository, IImageReadRepository imageReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _companyReadRepository = companyReadRepository;
            _userManager = userManager;
            _productWriteRepository = productWriteRepository;
            _imageWriteRepository = imageWriteRepository;
            _imageReadRepository = imageReadRepository;
        }
        #endregion

        #region Queries

        public async Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var ids = await GetCategoryIds(categoryId);
            var products = await _productReadRepository.Table
                .Where(p => p.CategoryId.HasValue && ids.Contains(p.CategoryId.Value))
                .ToListAsync();

            return products;
        }
        public async Task<GetByIdProductQueryResponse> GetProductByIdWithImageAndCategory(GetByIdProductQueryRequest request)
        {
            var product = await _productReadRepository.GetAll()
                .Include(p => p.Category)
                .Where(x => x.Id.ToString() == request.ProductId && x.DeletedDate == null)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }
            var images = await _imageReadRepository
                .GetAll()
                .Where(x => x.EntityId == request.ProductId &&
                x.EntityType == product.GetType().Name && x.DeletedDate == null)
                .ToListAsync();
            return new()
            {
                Product = product,
                Images = images
            };
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
            var user = await _userManager.FindByNameAsync(userName);

            Guid companyID = await _companyReadRepository.Table
                .Where(x => x.PrimaryAppUserID == user.Id || x.SecondaryAppUserID == user.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var products = await _productReadRepository.Table
                .Where(x => x.CompanyId == companyID && x.DeletedDate == null)
                .OrderBy(x => x.CreatedDate)
                .ToListAsync();

            return products;
        }
        #endregion

        #region Commands
        public async Task<bool> CreateProductAsync(CreateProductDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return false;
            }
            //userın company'sini getir
            var company = await _companyReadRepository.GetAll()
            .FirstOrDefaultAsync(x => x.PrimaryAppUserID == user.Id || x.SecondaryAppUserID == user.Id);

            Product p = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ProductCode = model.ProductCode,
                CategoryId = model.CategoryId,
                CompanyId = company.Id
            };

            bool responseProduct = await _productWriteRepository.AddAsync(p);
            int res = await _productWriteRepository.SaveAsync();

            if (res > 0)
            {
                bool responseImage = await SaveImages(model.ProductImages, p.Id.ToString(), p.GetType());
                if (responseImage && responseProduct)
                {
                    return true;
                }
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateProductAsync(UpdateProductCommandRequest request)
        {

            var product = await _productReadRepository.GetByIdAsync(request.ProductId);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.ProductCode = request.ProductCode;
            product.CategoryId = request.CategoryId;
            bool responseProduct = _productWriteRepository.Update(product);
            int prodRes = await _productWriteRepository.SaveAsync();
            if (prodRes > 0)
            {
                if (request.ProductImages != null && request.ProductImages.Any())
                {
                    bool responseImage = await SaveImages(request.ProductImages, product.Id.ToString(), product.GetType());

                    if (responseImage && responseProduct)
                    {
                        return true;
                    }
                }
                return true;
            }

            return false;

        }
        public async Task<bool> DeleteById(Guid productId)
        {
            var product = await _productReadRepository.GetByIdAsync(productId.ToString());
            var res = _productWriteRepository.Remove(product);
            await _productWriteRepository.SaveAsync();
            return res;
        }
        private async Task<bool> SaveImages(List<IFormFile> imageList, string entityID, Type entityType)
        {
            if (imageList != null && imageList.Any())
            {
                foreach (var item in imageList)
                {
                    if (!string.IsNullOrEmpty(item.FileName))
                    {
                        string directory = Path.Combine("wwwroot", "images", entityType.Name);
                        //{DateTime.UtcNow:yyyyMMdd}
                        string fileName = $"{entityID}_{item.FileName}";
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
                            EntityType = entityType.Name,
                            EntityId = entityID
                        };
                        await _imageWriteRepository.AddAsync(img);
                    }
                }
                int result = await _imageWriteRepository.SaveAsync();
                if (result > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<GetProductsByDefaultFilterQueryResponse>> GetProductsByDefaultFilter(GetProductsByDefaultFilterQueryRequest request)
        {
            if (request.CurrentPage < 1 || request.PageSize<1)
            {
                return null;
            }
            var product = await _productReadRepository.GetAll()
                .OrderByDescending(x => x.CreatedDate)
                .Skip((request.CurrentPage-1)*request.PageSize)
                .Take(request.PageSize)
                .Select(x=>new GetProductsByDefaultFilterQueryResponse()
                {
                    ProductId=x.Id.ToString(),
                    ProductName=x.Name,
                    ProductPrice =x.Price,
                    ProductDescription = x.Description ?? ""
                })
                .ToListAsync();
            if(product == null)
            {
                return null;
            }

            foreach (var item in product)
            {
                var image = await _imageReadRepository.Table.FirstOrDefaultAsync(x => x.EntityId==item.ProductId);
                if (image != null)
                {
                    item.ProductUrl = image.ImageUrl ?? "";
                }
            }
            return product;
        }
        #endregion
    }
}


/*
 
 public async Task<bool> CreateProductAsync(CreateProductDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return false;
            }
            //userın company'sini getir
            var company = await _companyReadRepository.GetAll()
            .FirstOrDefaultAsync(x => x.PrimaryAppUserID == user.Id || x.SecondaryAppUserID == user.Id);

            Product p = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ProductCode = model.ProductCode,
                CategoryId = model.CategoryId,
                CompanyId = company.Id
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
                            long date = DateTimeOffset.Now.ToUnixTimeSeconds();

                            string directory = Path.Combine("wwwroot", "images", p.GetType().Name);
                            //{DateTime.UtcNow:yyyyMMdd}
                            string fileName = $"{p.Id}_{date}_{item.FileName}";

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
 
 */
