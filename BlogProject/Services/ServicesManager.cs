using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
	public sealed class ServiceManager : IServiceManager
	{
		private readonly Lazy<IPostService> _lazyPostService;
		private readonly Lazy<ICategoryService> _lazyCategoryService;
		private readonly Lazy<IPostCategoryService> _lazyPostCategoryService;

		public ServiceManager(IPostRepository postRepository, ICategoryRepository categoryRepository, IPostCategoryRepository postCategoryRepository)
		{
			_lazyPostService = new Lazy<IPostService>(() => new PostService(postRepository));
			_lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(categoryRepository));
			_lazyPostCategoryService = new Lazy<IPostCategoryService>(() => new PostCategoryService(postCategoryRepository));
		}
		
		public IPostService PostService => _lazyPostService.Value;

		public ICategoryService CategoryService => _lazyCategoryService.Value;

		public IPostCategoryService PostCategoryService => _lazyPostCategoryService.Value;
	}
}
