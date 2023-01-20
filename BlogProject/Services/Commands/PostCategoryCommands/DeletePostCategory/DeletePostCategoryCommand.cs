using Contracts.Dtos.PostCategoryDtos;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Services.Commands.PostCategoryCommands.DeletePostCategory
{
	public class DeletePostCategoryCommand : IRequest<PostCategoryDto>
	{
		public int Id { get; set; }

		public class DeletePostCategoryCommandHandler : IRequestHandler<DeletePostCategoryCommand, PostCategoryDto>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;

			public DeletePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<PostCategoryDto> Handle(DeletePostCategoryCommand request, CancellationToken cancellationToken)
			{
				var postCategory = await _postCategoryRepository.GetAsync(postCategory => postCategory.Id == request.Id);
				if(postCategory is null) throw new PostCategoryNotFoundException(request.Id);
				
				var deletedPostCategory = await _postCategoryRepository.DeleteAsync(postCategory);
				var mappedPostCategory = deletedPostCategory.Adapt<PostCategoryDto>();
				return mappedPostCategory;
			}
		}
	}
}
