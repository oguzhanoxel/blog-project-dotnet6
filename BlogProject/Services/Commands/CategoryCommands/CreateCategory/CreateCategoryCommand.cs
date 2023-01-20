using Contracts.Dtos.CategoryDtos;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.CategoryCommands.CreateCategory
{
	public class CreateCategoryCommand : IRequest<CategoryDto>
	{
		public string? Title { get; set; }
		public string? Description { get; set; }

		public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
		{
			private readonly ICategoryRepository _categoryRepository;

			public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}

			public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
			{
				var category = request.Adapt<Category>();
				var createdCategory = await _categoryRepository.CreateAsync(category);
				var mappedCategory = createdCategory.Adapt<CategoryDto>();
				return mappedCategory;
			}
		}
	}
}
