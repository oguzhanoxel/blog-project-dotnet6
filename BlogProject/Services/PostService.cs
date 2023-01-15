using Contracts.Dtos.PostDtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services
{
	internal sealed class PostService : IPostService
	{
		private readonly IRepositoryManager _repositoryManager;

		public PostService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}

		public async Task<IEnumerable<PostListDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var posts = await _repositoryManager.PostRepository.GetAllAsync(cancellationToken);
			var postListDto = posts.Adapt<IEnumerable<PostListDto>>();
			return postListDto;
		}

		public async Task<PostDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var post = await _repositoryManager.PostRepository.GetByIdAsync(id, cancellationToken);

			if (post is null)
			{
				throw new PostNotFoundException(id);
			}

			var postDto = post.Adapt<PostDto>();
			return postDto;
		}

		public async Task<PostDto> CreateAsync(PostCreateDto postCreateDto, CancellationToken cancellationToken = default)
		{
			var post = postCreateDto.Adapt<Post>();
			_repositoryManager.PostRepository.Insert(post);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return post.Adapt<PostDto>();
		}

		public async Task<PostDto> UpdateAsync(int id, PostUpdateDto postUpdateDto, CancellationToken cancellationToken = default)
		{
			var post = await _repositoryManager.PostRepository.GetByIdAsync(id, cancellationToken);

			if(post is null)
			{
				throw new PostNotFoundException(id);
			}

			post.Title = postUpdateDto.Title;
			post.Text = postUpdateDto.Text;

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return post.Adapt<PostDto>();
		}

		public async Task<PostDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var post = await _repositoryManager.PostRepository.GetByIdAsync(id, cancellationToken);

			if(post is null)
			{
				throw new PostNotFoundException(id);
			}

			_repositoryManager.PostRepository.Remove(post);

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return post.Adapt<PostDto>();
		}
	}
}
