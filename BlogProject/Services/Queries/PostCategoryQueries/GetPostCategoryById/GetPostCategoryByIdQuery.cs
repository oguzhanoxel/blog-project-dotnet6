using Contracts.Dtos.PostCategoryDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Rules;

namespace Services.Queries.PostCategoryQueries.GetPostCategoryById
{
	public class GetPostCategoryByIdQuery : IRequest<PostCategoryDto>
	{
		public int Id { get; set; }

		public class GetPostCategoryByIdQueryHandler : IRequestHandler<GetPostCategoryByIdQuery, PostCategoryDto>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;
			private readonly PostCategoryBusinessRules _postCategoryBusinessRules;

			public GetPostCategoryByIdQueryHandler(IPostCategoryRepository postCategoryRepository, PostCategoryBusinessRules postCategoryBusinessRules)
			{
				_postCategoryRepository = postCategoryRepository;
				_postCategoryBusinessRules = postCategoryBusinessRules;
			}

			public async Task<PostCategoryDto> Handle(GetPostCategoryByIdQuery request, CancellationToken cancellationToken)
			{
				await _postCategoryBusinessRules.PostCategoryShouldExistWhenRequested(request.Id);

				var postCategory = await _postCategoryRepository.GetAsync(postCategory => postCategory.Id == request.Id);

				var mappedPostCategory = postCategory?.Adapt<PostCategoryDto>();
				return mappedPostCategory;
			}
		}
	}
}
