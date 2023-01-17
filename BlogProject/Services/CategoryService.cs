using Contracts.Dtos.CategoryDtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services
{
	internal sealed class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<IEnumerable<CategoryListDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var categories = await _categoryRepository.GetListAsync(cancellationToken:cancellationToken);
			var categoryListDto = categories.Adapt<IEnumerable<CategoryListDto>>();
			return categoryListDto;
		}

		public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var category = await _categoryRepository.GetAsync(c => c.Id == id, cancellationToken);

			if (category is null)
			{
				throw new CategoryNotFoundException(id);
			}

			var categoryDto = category.Adapt<CategoryDto>();
			return categoryDto;
		}

		public async Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken = default)
		{
			var mappedCategory = categoryCreateDto.Adapt<Category>();
			var category = await _categoryRepository.CreateAsync(mappedCategory);
			return category.Adapt<CategoryDto>();
		}

		public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken = default)
		{
			var category = await _categoryRepository.GetAsync(c => c.Id == id, cancellationToken);

			if(category is null)
			{
				throw new CategoryNotFoundException(id);
			}

			category.Title = categoryUpdateDto.Title;
			category.Description = categoryUpdateDto.Description;

			await _categoryRepository.UpdateAsync(category);

			return category.Adapt<CategoryDto>();
		}

		public async Task<CategoryDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var category = await _categoryRepository.GetAsync(c => c.Id == id, cancellationToken);

			if(category is null)
			{
				throw new CategoryNotFoundException(id);
			}

			await _categoryRepository.DeleteAsync(category);

			return category.Adapt<CategoryDto>();
		}
	}
}
