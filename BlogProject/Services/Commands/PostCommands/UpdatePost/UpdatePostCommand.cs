using Contracts.Dtos.PostDtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.PostCommands.UpdatePost
{
	public class UpdatePostCommand : IRequest<PostDto>
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Text { get; set; }

		public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostDto>
		{
			private readonly IPostRepository _postRepository;

			public UpdatePostCommandHandler(IPostRepository postRepository)
			{
				_postRepository = postRepository;
			}

			public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
			{
				var post = await _postRepository.GetAsync(post => post.Id == request.Id);
				if(post is null) throw new PostNotFoundException(request.Id);

				post.Title = request.Title;
				post.Text = request.Text;

				var updatedPost = await _postRepository.UpdateAsync(post);
				var mappedPost = updatedPost.Adapt<PostDto>();
				return mappedPost;
			}
		}
	}
}
