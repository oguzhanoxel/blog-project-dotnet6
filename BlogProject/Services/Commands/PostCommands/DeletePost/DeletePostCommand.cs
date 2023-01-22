using Contracts.Dtos.PostDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.PostCommands.DeletePost
{
	public class DeletePostCommand : IRequest<PostDto>
	{
		public int Id { get; set; }

		public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, PostDto>
		{
			private readonly IPostRepository _postRepository;

			public DeletePostCommandHandler(IPostRepository postRepository)
			{
				_postRepository = postRepository;
			}

			public async Task<PostDto> Handle(DeletePostCommand request, CancellationToken cancellationToken)
			{
				var post = await _postRepository.GetAsync(post => post.Id == request.Id);
				if(post is null) throw new NotFoundException("Post Not Found.");

				var deletedPost = await _postRepository.DeleteAsync(post);
				var mappedPost = deletedPost.Adapt<PostDto>();
				return mappedPost;
			}
		}
	}
}
