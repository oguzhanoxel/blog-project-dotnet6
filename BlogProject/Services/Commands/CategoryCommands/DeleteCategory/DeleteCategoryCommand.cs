using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.CategoryDtos;
using Services.Rules;

namespace Services.Commands.CategoryCommands.DeleteCategory
{
	public class DeleteCategoryCommand : IRequest<CategoryResponseDto>
	{
		public int Id { get; set; }

		public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryResponseDto>
		{
			private readonly ICategoryRepository _categoryRepository;
			private readonly CategoryBusinessRules _categoryBusinessRules;

			public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
			{
				_categoryRepository = categoryRepository;
				_categoryBusinessRules = categoryBusinessRules;
			}

			public async Task<CategoryResponseDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
			{
				await _categoryBusinessRules.CategoryShouldExistWhenRequested(request.Id);
				await _categoryBusinessRules.CategoryHasNoPostWhenDeleted(request.Id);

				var category = await _categoryRepository.GetAsync(category => category.Id == request.Id);
				
				await _categoryRepository.DeleteAsync(category);
				var mappedCategory = category.Adapt<CategoryResponseDto>();
				return mappedCategory;
			}
		}
	}
}
