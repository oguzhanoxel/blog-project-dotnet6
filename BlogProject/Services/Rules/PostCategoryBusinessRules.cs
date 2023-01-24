using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Services.Constants;

namespace Services.Rules
{
	public class PostCategoryBusinessRules
	{
		private readonly IPostCategoryRepository _postCategoryRepository;

		public PostCategoryBusinessRules(IPostCategoryRepository postCategoryRepository)
		{
			_postCategoryRepository = postCategoryRepository;
		}

		public async Task PostCategoryShouldExistWhenRequested(int id)
		{
			var pc = await _postCategoryRepository.GetAsync(pc => pc.Id == id);
			if (pc is null) throw new BusinessException(ExceptionConstants.PostCategoryNotFound);
		}
	}
}
