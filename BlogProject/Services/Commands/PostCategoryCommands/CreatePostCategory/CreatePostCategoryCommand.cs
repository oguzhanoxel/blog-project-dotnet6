using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using Services.Dtos.PostCategoryDtos;

namespace Services.Commands.PostCategoryCommands.CreatePostCategory
{
	public class CreatePostCategoryCommand : IRequest<PostCategoryResponseDto>
	{
		public int PostId { get; set; }
		public int CategoryId { get; set; }

		public class CreatePostCategoryCommandHandler : IRequestHandler<CreatePostCategoryCommand, PostCategoryResponseDto>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;

			public CreatePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<PostCategoryResponseDto> Handle(CreatePostCategoryCommand request, CancellationToken cancellationToken)
			{
				var postCategory = request.Adapt<PostCategory>();
				var createdPostCategory = await _postCategoryRepository.CreateAsync
				(postCategory);
				var mappedPostCategory = createdPostCategory.Adapt<PostCategoryResponseDto>();
				return mappedPostCategory;
			}
		}
	}
}
