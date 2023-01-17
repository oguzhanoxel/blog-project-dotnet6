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
		private readonly IPostRepository _postRepository;

		public PostService(IPostRepository postRepository)
		{
			_postRepository = postRepository;
		}

		public async Task<IEnumerable<PostListDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var posts = await _postRepository.GetListAsync(cancellationToken:cancellationToken);
			var postListDto = posts.Adapt<IEnumerable<PostListDto>>();
			return postListDto;
		}

		public async Task<PostDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var post = await _postRepository.GetAsync(p => p.Id == id, cancellationToken);

			if (post is null)
			{
				throw new PostNotFoundException(id);
			}

			var postDto = post.Adapt<PostDto>();
			return postDto;
		}

		public async Task<PostDto> CreateAsync(PostCreateDto postCreateDto, CancellationToken cancellationToken = default)
		{
			var mappedPost = postCreateDto.Adapt<Post>();
			var post = await _postRepository.CreateAsync(mappedPost);
			return post.Adapt<PostDto>();
		}

		public async Task<PostDto> UpdateAsync(int id, PostUpdateDto postUpdateDto, CancellationToken cancellationToken = default)
		{
			var post = await _postRepository.GetAsync(p => p.Id == id, cancellationToken);

			if(post is null)
			{
				throw new PostNotFoundException(id);
			}

			post.Title = postUpdateDto.Title;
			post.Text = postUpdateDto.Text;

			await _postRepository.UpdateAsync(post);

			return post.Adapt<PostDto>();
		}

		public async Task<PostDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var post = await _postRepository.GetAsync(p => p.Id == id, cancellationToken);

			if(post is null)
			{
				throw new PostNotFoundException(id);
			}

			await _postRepository.DeleteAsync(post);

			return post.Adapt<PostDto>();
		}
	}
}
