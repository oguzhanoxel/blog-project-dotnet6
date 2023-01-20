using Contracts.Dtos.PostCategoryDtos;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.PostCategoryCommands.CreatePostCategory
{
	public class CreatePostCategoryCommand : IRequest<PostCategoryDto>
	{
		public int PostId { get; set; }
		public int CategoryId { get; set; }

		public class CreatePostCategoryCommandHandler : IRequestHandler<CreatePostCategoryCommand, PostCategoryDto>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;

			public CreatePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<PostCategoryDto> Handle(CreatePostCategoryCommand request, CancellationToken cancellationToken)
			{
				var postCategory = request.Adapt<PostCategory>();
				var createdPostCategory = await _postCategoryRepository.CreateAsync
				(postCategory);
				var mappedPostCategory = createdPostCategory.Adapt<PostCategoryDto>();
				return mappedPostCategory;
			}
		}
	}
}
