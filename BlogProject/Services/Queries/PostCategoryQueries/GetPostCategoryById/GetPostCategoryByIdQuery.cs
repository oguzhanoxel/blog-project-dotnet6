using Contracts.Dtos.PostCategoryDtos;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Queries.PostCategoryQueries.GetPostCategoryById
{
	public class GetPostCategoryByIdQuery : IRequest<PostCategoryDto>
	{
		public int Id { get; set; }

		public class GetPostCategoryByIdQueryHandler : IRequestHandler<GetPostCategoryByIdQuery, PostCategoryDto>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;

			public GetPostCategoryByIdQueryHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<PostCategoryDto> Handle(GetPostCategoryByIdQuery request, CancellationToken cancellationToken)
			{
				var postCategory = await _postCategoryRepository.GetAsync(postCategory => postCategory.Id == request.Id);
				var mappedPostCategory = postCategory.Adapt<PostCategoryDto>();
				return mappedPostCategory;
			}
		}
	}
}
