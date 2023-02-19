using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.CategoryDtos;

namespace Services.Commands.CategoryCommands.CreateCategory
{
	public class CreateCategoryCommand : IRequest<CategoryResponseDto>
	{
		public string Title { get; set; }

		public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponseDto>
		{
			private readonly ICategoryRepository _categoryRepository;

			public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}

			public async Task<CategoryResponseDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
			{
				var category = request.Adapt<Category>();
				var createdCategory = await _categoryRepository.CreateAsync(category);
				var mappedCategory = createdCategory.Adapt<CategoryResponseDto>();
				return mappedCategory;
			}
		}
	}
}
