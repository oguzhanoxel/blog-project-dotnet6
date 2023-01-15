using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
	public sealed class ServiceManager : IServiceManager
	{
		private readonly Lazy<IPostService> _lazyPostService;
		private readonly Lazy<ICategoryService> _lazyCategoryService;
		private readonly Lazy<IPostCategoryService> _lazyPostCategoryService;

		public ServiceManager(IRepositoryManager repositoryManager)
		{
			_lazyPostService = new Lazy<IPostService>(() => new PostService(repositoryManager));
			_lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager));
			_lazyPostCategoryService = new Lazy<IPostCategoryService>(() => new PostCategoryService(repositoryManager));
		}
		
		public IPostService PostService => _lazyPostService.Value;

		public ICategoryService CategoryService => _lazyCategoryService.Value;

		public IPostCategoryService PostCategoryService => _lazyPostCategoryService.Value;
	}
}
