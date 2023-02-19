using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.PostDtos;

namespace Services.Commands.PostCommands.CreatePost
{
	public class CreatePostCommand : IRequest<PostResponseDto>
	{
		public string Title { get; set; }
		public string Text { get; set; }
		
		public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostResponseDto>
		{
			private readonly IPostRepository _postRepository;

			public CreatePostCommandHandler(IPostRepository postRepository)
			{
				_postRepository = postRepository;
			}

			public async Task<PostResponseDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
			{
				var post = request.Adapt<Post>();
				var createdPost = await _postRepository.CreateAsync(post);
				var mappedPost = createdPost.Adapt<PostResponseDto>();

				return mappedPost;
			}
		}
	}
}
