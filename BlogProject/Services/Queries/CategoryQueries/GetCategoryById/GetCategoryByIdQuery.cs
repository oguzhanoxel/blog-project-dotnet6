using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.CategoryDtos;
using Services.Rules;

namespace Services.Queries.CategoryQueries.GetCategoryById
{
	public class GetCategoryByIdQuery : IRequest<CategoryResponseDto>
	{
		public int Id { get; set; }

		public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDto>
		{
			private readonly ICategoryRepository _categoryRepository;
			private readonly CategoryBusinessRules _categoryBusinessRules;

			public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
			{
				_categoryRepository = categoryRepository;
				_categoryBusinessRules = categoryBusinessRules;
			}


			public async Task<CategoryResponseDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
			{
				await _categoryBusinessRules.CategoryShouldExistWhenRequested(request.Id);

				var category = await _categoryRepository.GetAsync(category => category.Id == request.Id);

				var mappedCategory = category.Adapt<CategoryResponseDto>();
				return mappedCategory;
			}
		}
	}
}
