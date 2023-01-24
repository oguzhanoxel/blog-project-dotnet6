using Contracts.Dtos.PostDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Rules;

namespace Services.Commands.PostCommands.UpdatePost
{
	public class UpdatePostCommand : IRequest<PostDto>
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }

		public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostDto>
		{
			private readonly IPostRepository _postRepository;
			private readonly PostBusinessRules _postBusinessRules;

			public UpdatePostCommandHandler(IPostRepository postRepository, PostBusinessRules postBusinessRules)
			{
				_postRepository = postRepository;
				_postBusinessRules = postBusinessRules;
			}


			public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
			{
				await _postBusinessRules.PostShouldExistWhenRequested(request.Id);

				var post = await _postRepository.GetAsync(post => post.Id == request.Id);

				post.Title = request.Title;
				post.Text = request.Text;

				var updatedPost = await _postRepository.UpdateAsync(post);
				var mappedPost = updatedPost.Adapt<PostDto>();
				return mappedPost;
			}
		}
	}
}
