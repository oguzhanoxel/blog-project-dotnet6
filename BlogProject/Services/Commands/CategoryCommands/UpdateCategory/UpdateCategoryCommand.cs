using Contracts.Dtos.CategoryDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Rules;

namespace Services.Commands.CategoryCommands.UpdateCategory
{
	public class UpdateCategoryCommand : IRequest<CategoryDto>
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
		{
			private readonly ICategoryRepository _categoryRepository;
			private readonly CategoryBusinessRules _categoryBusinessRules;

			public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
			{
				_categoryRepository = categoryRepository;
				_categoryBusinessRules = categoryBusinessRules;
			}

			public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
			{
				await _categoryBusinessRules.CategoryShouldExistWhenRequested(request.Id);

				var category = await _categoryRepository.GetAsync(category => category.Id == request.Id);

				category.Title = request.Title;
				category.Description = request.Description;

				var updatedCategory = await _categoryRepository.UpdateAsync(category);
				var mappedCategory = updatedCategory.Adapt<CategoryDto>();
				return mappedCategory;
			}
		}
	}
}
