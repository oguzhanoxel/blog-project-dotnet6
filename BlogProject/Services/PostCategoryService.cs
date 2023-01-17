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
		private readonly IPostCategoryRepository _postCategoryRepository;

		public PostCategoryService(IPostCategoryRepository postCategoryRepository)
		{
			_postCategoryRepository = postCategoryRepository;
		}

		public async Task<IEnumerable<PostCategoryListDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var postCategories = await _postCategoryRepository.GetListAsync(cancellationToken:cancellationToken);
			var postCategoryListDto = postCategories.Adapt<IEnumerable<PostCategoryListDto>>();
			return postCategoryListDto;
		}

		public async Task<PostCategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var postCategory = await _postCategoryRepository.GetAsync(p => p.Id == id, cancellationToken);

			if (postCategory is null)
			{
				throw new PostCategoryNotFoundException(id);
			}

			var postCategoryDto = postCategory.Adapt<PostCategoryDto>();
			return postCategoryDto;
		}

		public async Task<PostCategoryDto> CreateAsync(PostCategoryCreateDto postCategoryCreateDto, CancellationToken cancellationToken = default)
		{
			var mappedPostCategory = postCategoryCreateDto.Adapt<PostCategory>();
			var postCategory = await _postCategoryRepository.CreateAsync(mappedPostCategory);
			return postCategory.Adapt<PostCategoryDto>();
		}

		public async Task<PostCategoryDto> UpdateAsync(int id, PostCategoryUpdateDto postCategoryUpdateDto, CancellationToken cancellationToken = default)
		{
			var postCategory = await _postCategoryRepository.GetAsync(p => p.Id == id, cancellationToken);

			if(postCategory is null)
			{
				throw new PostCategoryNotFoundException(id);
			}

			postCategory.PostId = postCategoryUpdateDto.PostId;
			postCategory.CategoryId = postCategoryUpdateDto.CategoryId;

			await _postCategoryRepository.UpdateAsync(postCategory);

			return postCategory.Adapt<PostCategoryDto>();
		}

		public async Task<PostCategoryDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var postCategory = await _postCategoryRepository.GetAsync(p => p.Id == id, cancellationToken);

			if(postCategory is null)
			{
				throw new PostCategoryNotFoundException(id);
			}

			await _postCategoryRepository.DeleteAsync(postCategory);

			return postCategory.Adapt<PostCategoryDto>();
		}
	}
}
