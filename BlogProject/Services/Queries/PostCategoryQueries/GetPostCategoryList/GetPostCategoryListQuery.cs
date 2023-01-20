using Contracts.Dtos.PostCategoryDtos;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Queries.PostCategoryQueries.GetPostCategoryList
{
	public class GetPostCategoryListQuery : IRequest<IList<PostCategoryDto>>
	{

		public class GetPostCategoryListQueryHandler : IRequestHandler<GetPostCategoryListQuery, IList<PostCategoryDto>>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;

			public GetPostCategoryListQueryHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<IList<PostCategoryDto>> Handle(GetPostCategoryListQuery request, CancellationToken cancellationToken)
			{
				var postCategories = await _postCategoryRepository.GetListAsync();
				var mappedPostCategories = postCategories.Adapt<IList<PostCategoryDto>>();
				return mappedPostCategories;
			}
		}
	}
}
