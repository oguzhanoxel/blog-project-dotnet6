using Contracts.Dtos.PostDtos;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.PostCommands.CreatePost
{
	public class CreatePostCommand : IRequest<PostDto>
	{
		public string Title { get; set; }
		public string Text { get; set; }
		
		public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
		{
			private readonly IPostRepository _postRepository;

			public CreatePostCommandHandler(IPostRepository postRepository)
			{
				_postRepository = postRepository;
			}

			public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
			{
				var post = request.Adapt<Post>();
				var createdPost = await _postRepository.CreateAsync(post);
				var mappedPost = createdPost.Adapt<PostDto>();

				return mappedPost;
			}
		}
	}
}
