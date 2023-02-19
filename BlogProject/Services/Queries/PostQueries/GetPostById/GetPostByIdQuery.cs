using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.PostDtos;
using Services.Rules;

namespace Services.Queries.PostQueries.GetPostById
{
	public class GetPostByIdQuery : IRequest<PostResponseDto>
	{
		public int Id { get; set; }

		public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostResponseDto>
		{			
			private readonly IPostRepository _postRepository;
			private readonly PostBusinessRules _postBusinessRules;

			public GetPostByIdQueryHandler(IPostRepository postRepository, PostBusinessRules postBusinessRules)
			{
				_postRepository = postRepository;
				_postBusinessRules = postBusinessRules;
			}

			public async Task<PostResponseDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
			{
				await _postBusinessRules.PostShouldExistWhenRequested(request.Id);

				var post = await _postRepository.GetAsync(post => post.Id == request.Id);

				var mappedPost = post.Adapt<PostResponseDto>();
				return mappedPost;
			}
		}
	}
}
