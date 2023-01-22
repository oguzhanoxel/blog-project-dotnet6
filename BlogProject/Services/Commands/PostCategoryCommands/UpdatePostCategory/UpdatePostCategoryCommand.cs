using Contracts.Dtos.PostCategoryDtos;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

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

			public UpdatePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<PostCategoryDto> Handle(UpdatePostCategoryCommand request, CancellationToken cancellationToken)
			{
				var postCategory = await _postCategoryRepository.GetAsync(postCategory => postCategory.Id == request.Id);
				if(postCategory is null) throw new NotFoundException("PostCategory Not Found.");

				postCategory.PostId = request.PostId;
				postCategory.CategoryId = request.CategoryId;

				var updatedPostCategory = await _postCategoryRepository.UpdateAsync(postCategory);
				var mappedPostCategory = updatedPostCategory.Adapt<PostCategoryDto>();
				return mappedPostCategory;
			}
		}
	}
}
