using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.CategoryDtos;

namespace Services.Queries.CategoryQueries.GetCategoryList
{
	public class GetCategoryListQuery : IRequest<IList<CategoryResponseDto>>
	{

		public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, IList<CategoryResponseDto>>
		{
			private readonly ICategoryRepository _categoryRepository;

			public GetCategoryListQueryHandler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}

			public async Task<IList<CategoryResponseDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
			{
				var categories = await _categoryRepository.GetListAsync();
				var mappedCategories = categories.Adapt<IList<CategoryResponseDto>>();
				return mappedCategories;
			}
		}
	}
}
