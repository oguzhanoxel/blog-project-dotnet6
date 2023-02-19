using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.PostDtos;

namespace Services.Queries.PostQueries.GetPostList
{
	public class GetPostListQuery : IRequest<IList<PostResponseDto>>
	{

		public class GetPostListQueryHandler : IRequestHandler<GetPostListQuery, IList<PostResponseDto>>
		{
			private readonly IPostRepository _postRepository;

			public GetPostListQueryHandler(IPostRepository postRepository)
			{
				_postRepository = postRepository;
			}

			public async Task<IList<PostResponseDto>> Handle(GetPostListQuery request, CancellationToken cancellationToken)
			{
				var posts = await _postRepository.GetListAsync();
				var mappedPosts = posts.Adapt<IList<PostResponseDto>>();
				return mappedPosts;
			}
		}
	}
}
