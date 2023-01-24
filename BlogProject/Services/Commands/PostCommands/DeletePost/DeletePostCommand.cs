using Contracts.Dtos.PostDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Rules;

namespace Services.Commands.PostCommands.DeletePost
{
	public class DeletePostCommand : IRequest<PostDto>
	{
		public int Id { get; set; }

		public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, PostDto>
		{
			private readonly IPostRepository _postRepository;
			private readonly PostBusinessRules _postBusinessRules;

			public DeletePostCommandHandler(IPostRepository postRepository, PostBusinessRules postBusinessRules)
			{
				_postRepository = postRepository;
				_postBusinessRules = postBusinessRules;
			}

			public async Task<PostDto> Handle(DeletePostCommand request, CancellationToken cancellationToken)
			{
				await _postBusinessRules.PostShouldExistWhenRequested(request.Id);

				var post = await _postRepository.GetAsync(post => post.Id == request.Id);

				var deletedPost = await _postRepository.DeleteAsync(post);
				var mappedPost = deletedPost.Adapt<PostDto>();
				return mappedPost;
			}
		}
	}
}