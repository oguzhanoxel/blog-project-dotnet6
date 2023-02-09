using Contracts.Dtos.PostCategoryDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Rules;

namespace Services.Commands.PostCategoryCommands.DeletePostCategory
{
	public class DeletePostCategoryCommand : IRequest<PostCategoryDto>
	{
		public int Id { get; set; }

		public class DeletePostCategoryCommandHandler : IRequestHandler<DeletePostCategoryCommand, PostCategoryDto>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;
			private readonly PostCategoryBusinessRules _postCategoryBusinessRules;

			public DeletePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository, PostCategoryBusinessRules postCategoryBusinessRules)
			{
				_postCategoryRepository = postCategoryRepository;
				_postCategoryBusinessRules = postCategoryBusinessRules;
			}

			public async Task<PostCategoryDto> Handle(DeletePostCategoryCommand request, CancellationToken cancellationToken)
			{
				await _postCategoryBusinessRules.PostCategoryShouldExistWhenRequested(request.Id);

				var postCategory = await _postCategoryRepository.GetAsync(postCategory => postCategory.Id == request.Id);

				await _postCategoryRepository.DeleteAsync(postCategory);
				var mappedPostCategory = postCategory.Adapt<PostCategoryDto>();
				return mappedPostCategory;
			}
		}
	}
}
