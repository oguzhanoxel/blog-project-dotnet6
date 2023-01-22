using Contracts.Dtos.CategoryDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.CategoryCommands.UpdateCategory
{
	public class UpdateCategoryCommand : IRequest<CategoryDto>
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }

		public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
		{
			private readonly ICategoryRepository _categoryRepository;

			public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}

			public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
			{
				var category = await _categoryRepository.GetAsync(category => category.Id == request.Id);
				if(category is null) throw new NotFoundException("Category Not Found.");

				category.Title = request.Title;
				category.Description = request.Description;

				var updatedCategory = await _categoryRepository.UpdateAsync(category);
				var mappedCategory = updatedCategory.Adapt<CategoryDto>();
				return mappedCategory;
			}
		}
	}
}
