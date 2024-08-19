using B2B_Project.Application.Common.Models;
using B2B_Project.Application.DTOs.Category;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Category.Queries.GetAll
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, HandlerResponse<GetAllCategoryQueryResponse>>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;

        public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<HandlerResponse<GetAllCategoryQueryResponse>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _categoryReadRepository.GetAll()
                .Select(x => new GetAllCategoryDto
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                }).ToListAsync();
            if(data != null )
            {
                GetAllCategoryQueryResponse response = new()
                {
                    Categories = data
                };

                return new()
                {
                    Data = response,
                };
            }
            return new()
            {
                Data = default,
                Message = "Bir Hata Oluştu"
            };

        }
    }

}
