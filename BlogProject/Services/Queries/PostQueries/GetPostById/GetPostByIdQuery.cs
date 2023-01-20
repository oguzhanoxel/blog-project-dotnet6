using Contracts.Dtos.PostDtos;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Queries.PostQueries.GetPostById
{
	public class GetPostByIdQuery : IRequest<PostDto>
	{
		public int Id { get; set; }

		public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
		{
			private readonly IPostRepository _postRepository;

			public GetPostByIdQueryHandler(IPostRepository postRepository)
			{
				_postRepository = postRepository;
			}

			public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
			{
				var post = await _postRepository.GetAsync(post => post.Id == request.Id);

				if(post is null) throw new PostNotFoundException(request.Id);

				var mappedPost = post.Adapt<PostDto>();
				return mappedPost;
			}
		}
	}
}
