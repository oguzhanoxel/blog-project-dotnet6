using Contracts.Dtos.PostCategoryDtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services
{
	internal sealed class PostCategoryService : IPostCategoryService
	{
		private readonly IRepositoryManager _repositoryManager;

		public PostCategoryService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}

		public async Task<IEnumerable<PostCategoryListDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var postCategories = await _repositoryManager.PostCategoryRepository.GetAllAsync(cancellationToken);
			var postCategoryListDto = postCategories.Adapt<IEnumerable<PostCategoryListDto>>();
			return postCategoryListDto;
		}

		public async Task<PostCategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var postCategory = await _repositoryManager.PostCategoryRepository.GetByIdAsync(id, cancellationToken);

			if (postCategory is null)
			{
				throw new PostCategoryNotFoundException(id);
			}

			var postCategoryDto = postCategory.Adapt<PostCategoryDto>();
			return postCategoryDto;
		}

		public async Task<PostCategoryDto> CreateAsync(PostCategoryCreateDto postCategoryCreateDto, CancellationToken cancellationToken = default)
		{
			var postCategory = postCategoryCreateDto.Adapt<PostCategory>();
			_repositoryManager.PostCategoryRepository.Insert(postCategory);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return postCategory.Adapt<PostCategoryDto>();
		}

		public async Task<PostCategoryDto> UpdateAsync(int id, PostCategoryUpdateDto postCategoryUpdateDto, CancellationToken cancellationToken = default)
		{
			var postCategory = await _repositoryManager.PostCategoryRepository.GetByIdAsync(id, cancellationToken);

			if(postCategory is null)
			{
				throw new PostCategoryNotFoundException(id);
			}

			postCategory.PostId = postCategoryUpdateDto.PostId;
			postCategory.CategoryId = postCategoryUpdateDto.CategoryId;

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return postCategory.Adapt<PostCategoryDto>();
		}

		public async Task<PostCategoryDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var postCategory = await _repositoryManager.PostCategoryRepository.GetByIdAsync(id, cancellationToken);

			if(postCategory is null)
			{
				throw new PostCategoryNotFoundException(id);
			}

			_repositoryManager.PostCategoryRepository.Remove(postCategory);

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return postCategory.Adapt<PostCategoryDto>();
		}
	}
}
