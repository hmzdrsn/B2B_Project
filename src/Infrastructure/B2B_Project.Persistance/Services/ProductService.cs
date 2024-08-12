using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance.Services
{
    public class ProductService : IProductService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        public ProductService(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
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
    }
}
