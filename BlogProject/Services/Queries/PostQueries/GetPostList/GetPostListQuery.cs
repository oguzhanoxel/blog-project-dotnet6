using Contracts.Dtos.PostDtos;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Queries.PostQueries.GetPostList
{
	public class GetPostListQuery : IRequest<IList<PostDto>>
	{

		public class GetPostListQueryHandler : IRequestHandler<GetPostListQuery, IList<PostDto>>
		{
			private readonly IPostRepository _postRepository;

			public GetPostListQueryHandler(IPostRepository postRepository)
			{
				_postRepository = postRepository;
			}

			public async Task<IList<PostDto>> Handle(GetPostListQuery request, CancellationToken cancellationToken)
			{
				var posts = await _postRepository.GetListAsync();
				var mappedPosts = posts.Adapt<IList<PostDto>>();
				return mappedPosts;
			}
		}
	}
}
