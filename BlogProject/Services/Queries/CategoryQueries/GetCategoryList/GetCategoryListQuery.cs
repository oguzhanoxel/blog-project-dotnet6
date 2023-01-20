using Contracts.Dtos.CategoryDtos;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Queries.CategoryQueries.GetCategoryList
{
	public class GetCategoryListQuery : IRequest<IList<CategoryListDto>>
	{

		public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, IList<CategoryListDto>>
		{
			private readonly ICategoryRepository _categoryRepository;

			public GetCategoryListQueryHandler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}

			public async Task<IList<CategoryListDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
			{
				var categories = await _categoryRepository.GetListAsync();
				var mappedCategories = categories.Adapt<IList<CategoryListDto>>();
				return mappedCategories;
			}
		}
	}
}
