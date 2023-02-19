using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.CategoryDtos;
using Services.Rules;

namespace Services.Commands.CategoryCommands.UpdateCategory
{
	public class UpdateCategoryCommand : IRequest<CategoryResponseDto>
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponseDto>
		{
			private readonly ICategoryRepository _categoryRepository;
			private readonly CategoryBusinessRules _categoryBusinessRules;

			public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
			{
				_categoryRepository = categoryRepository;
				_categoryBusinessRules = categoryBusinessRules;
			}

			public async Task<CategoryResponseDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
			{
				await _categoryBusinessRules.CategoryShouldExistWhenRequested(request.Id);

				var category = await _categoryRepository.GetAsync(category => category.Id == request.Id);

				category.Title = request.Title;

				var updatedCategory = await _categoryRepository.UpdateAsync(category);
				var mappedCategory = updatedCategory.Adapt<CategoryResponseDto>();
				return mappedCategory;
			}
		}
	}
}
