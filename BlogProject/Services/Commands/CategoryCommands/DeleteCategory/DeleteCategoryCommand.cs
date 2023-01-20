using Contracts.Dtos.CategoryDtos;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.CategoryCommands.DeleteCategory
{
	public class DeleteCategoryCommand : IRequest<CategoryDto>
	{
		public int Id { get; set; }

		public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryDto>
		{
			private readonly ICategoryRepository _categoryRepository;

			public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
			{
				_categoryRepository = categoryRepository;
			}

			public async Task<CategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
			{
				var category = await _categoryRepository.GetAsync(category => category.Id == request.Id);
				if(category is null) throw new CategoryNotFoundException(request.Id);

				var deletedCategory = await _categoryRepository.DeleteAsync(category);
				var mappedCategory = deletedCategory.Adapt<CategoryDto>();
				return mappedCategory;
			}
		}
	}
}
