using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Services.Constants;

namespace Services.Rules
{
	public class CategoryBusinessRules
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IPostCategoryRepository _postCategoryRepository;

		public CategoryBusinessRules(ICategoryRepository categoryRepository, IPostCategoryRepository postCategoryRepository)
		{
			_categoryRepository = categoryRepository;
			_postCategoryRepository = postCategoryRepository;
		}

		public async Task CategoryShouldExistWhenRequested(int id)
		{
			var category = await _categoryRepository.GetAsync(category => category.Id == id);
			if (category == null) throw new BusinessException(ExceptionConstants.CategoryNotFound);
		}

		public async Task CategoryHasNoPostWhenDeleted(int id)
		{
			var pc = await _postCategoryRepository.GetAsync(pc => pc.CategoryId == id);
			if (pc is not null) throw new BusinessException(ExceptionConstants.CategoryHasPost);
		}
	}
}
