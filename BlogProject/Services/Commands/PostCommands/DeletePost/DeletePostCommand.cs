using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.PostDtos;
using Services.Rules;

namespace Services.Commands.PostCommands.DeletePost
{
	public class DeletePostCommand : IRequest<PostResponseDto>
	{
		public int Id { get; set; }

		public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, PostResponseDto>
		{
			private readonly IPostRepository _postRepository;
			private readonly PostBusinessRules _postBusinessRules;

			public DeletePostCommandHandler(IPostRepository postRepository, PostBusinessRules postBusinessRules)
			{
				_postRepository = postRepository;
				_postBusinessRules = postBusinessRules;
			}

			public async Task<PostResponseDto> Handle(DeletePostCommand request, CancellationToken cancellationToken)
			{
				await _postBusinessRules.PostShouldExistWhenRequested(request.Id);

				var post = await _postRepository.GetAsync(post => post.Id == request.Id);

				await _postRepository.DeleteAsync(post);
				var mappedPost = post.Adapt<PostResponseDto>();
				return mappedPost;
			}
		}
	}
}
