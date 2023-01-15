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
		private readonly IRepositoryManager _repositoryManager;

		public CategoryService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}

		public async Task<IEnumerable<CategoryListDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var categories = await _repositoryManager.CategoryRepository.GetAllAsync(cancellationToken);
			var categoryListDto = categories.Adapt<IEnumerable<CategoryListDto>>();
			return categoryListDto;
		}

		public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var category = await _repositoryManager.CategoryRepository.GetByIdAsync(id, cancellationToken);

			if (category is null)
			{
				throw new CategoryNotFoundException(id);
			}

			var categoryDto = category.Adapt<CategoryDto>();
			return categoryDto;
		}

		public async Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken = default)
		{
			var category = categoryCreateDto.Adapt<Category>();
			_repositoryManager.CategoryRepository.Insert(category);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return category.Adapt<CategoryDto>();
		}

		public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken = default)
		{
			var category = await _repositoryManager.CategoryRepository.GetByIdAsync(id, cancellationToken);

			if(category is null)
			{
				throw new CategoryNotFoundException(id);
			}

			category.Title = categoryUpdateDto.Title;
			category.Description = categoryUpdateDto.Description;

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return category.Adapt<CategoryDto>();
		}

		public async Task<CategoryDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var category = await _repositoryManager.CategoryRepository.GetByIdAsync(id, cancellationToken);

			if(category is null)
			{
				throw new CategoryNotFoundException(id);
			}

			_repositoryManager.CategoryRepository.Remove(category);

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return category.Adapt<CategoryDto>();
		}
	}
}
