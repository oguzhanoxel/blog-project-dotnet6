using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Dtos.PostDtos;

namespace Services.Queries.PostQueries.GetPostListByCategoryId
{
	public class GetPostListByCategoryIdQuery : IRequest<IList<GetPostListByCategoryIdResponseDto>>
	{
		public int CategoryId { get; set; }

		public class GetPostListByCategoryIdQueryHandler : IRequestHandler<GetPostListByCategoryIdQuery, IList<GetPostListByCategoryIdResponseDto>>
		{
			private readonly IPostCategoryRepository _postCategoryRepository;

			public GetPostListByCategoryIdQueryHandler(IPostCategoryRepository postCategoryRepository)
			{
				_postCategoryRepository = postCategoryRepository;
			}

			public async Task<IList<GetPostListByCategoryIdResponseDto>> Handle(GetPostListByCategoryIdQuery request, CancellationToken cancellationToken)
			{
				var postCategories = await _postCategoryRepository.GetListAsync(
					pc => pc.Category.Id == request.CategoryId,
					include: pc =>
						pc.Include(pc => pc.Post).Include(pc => pc.Category));

				TypeAdapterConfig<PostCategory, GetPostListByCategoryIdResponseDto>.NewConfig()
					.Map(dest => dest.Title, src => src.Post.Title)
					.Map(dest => dest.Text, src => src.Post.Text)
					.Map(dest => dest.CategoryTitle, src => src.Category.Title);

				var mappedPosts = postCategories.Adapt<IList<GetPostListByCategoryIdResponseDto>>();
				//TODO: DO SOMETHİNG 
				return mappedPosts;
			}
		}
	}
}
