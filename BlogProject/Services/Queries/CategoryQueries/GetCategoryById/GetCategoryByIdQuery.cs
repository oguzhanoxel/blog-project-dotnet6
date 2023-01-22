using Contracts.Dtos.CategoryDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Queries.CategoryQueries.GetCategoryById
{
	public class GetCategoryByIdQuery : IRequest<CategoryDto>
	{
		public int Id { get; set; }

		public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
		{
			private readonly ICategoryRepository _categoryRepository;

			public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}

			public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
			{
				var category = await _categoryRepository.GetAsync(category => category.Id == request.Id);
				if(category is null) throw new NotFoundException("Category Not Found.");

				var mappedCategory = category.Adapt<CategoryDto>();
				return mappedCategory;
			}
		}
	}
}
