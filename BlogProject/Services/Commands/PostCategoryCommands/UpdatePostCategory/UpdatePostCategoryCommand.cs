using Contracts.Dtos.PostCategoryDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Rules;

namespace Services.Commands.PostCategoryCommands.UpdatePostCategory
{
	public class UpdatePostCategoryCommand : IRequest<PostCategoryDto>
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public int CategoryId { get; set; }

		public class UpdatePostCategoryCommandHandler : IRequestHandler<UpdatePostCategoryCommand, PostCategoryDto>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;
			private readonly PostCategoryBusinessRules _postCategoryBusinessRules;

			public UpdatePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository, PostCategoryBusinessRules postCategoryBusinessRules)
			{
				_postCategoryRepository = postCategoryRepository;
				_postCategoryBusinessRules = postCategoryBusinessRules;
			}
			public async Task<PostCategoryDto> Handle(UpdatePostCategoryCommand request, CancellationToken cancellationToken)
			{
				await _postCategoryBusinessRules.PostCategoryShouldExistWhenRequested(request.Id);

				var postCategory = await _postCategoryRepository.GetAsync(postCategory => postCategory.Id == request.Id);
				
				postCategory.PostId = request.PostId;
				postCategory.CategoryId = request.CategoryId;

				var updatedPostCategory = await _postCategoryRepository.UpdateAsync(postCategory);
				var mappedPostCategory = updatedPostCategory.Adapt<PostCategoryDto>();
				return mappedPostCategory;
			}
		}
	}
}
