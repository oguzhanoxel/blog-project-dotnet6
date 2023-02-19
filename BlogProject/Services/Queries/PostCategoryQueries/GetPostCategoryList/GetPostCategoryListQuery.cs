using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.PostCategoryDtos;

namespace Services.Queries.PostCategoryQueries.GetPostCategoryList
{
	public class GetPostCategoryListQuery : IRequest<IList<PostCategoryResponseDto>>
	{

		public class GetPostCategoryListQueryHandler : IRequestHandler<GetPostCategoryListQuery, IList<PostCategoryResponseDto>>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;

			public GetPostCategoryListQueryHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<IList<PostCategoryResponseDto>> Handle(GetPostCategoryListQuery request, CancellationToken cancellationToken)
			{
				var postCategories = await _postCategoryRepository.GetListAsync();
				var mappedPostCategories = postCategories.Adapt<IList<PostCategoryResponseDto>>();
				return mappedPostCategories;
			}
		}
	}
}
