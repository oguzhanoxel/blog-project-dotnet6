using Domain.Repositories;

namespace Persistence.Repositories
{
	public sealed class RepositoryManager : IRepositoryManager
	{
		private readonly Lazy<IPostRepository> _lazyPostRepository;
		private readonly Lazy<ICategoryRepository> _lazyCategoryRepository;
		private readonly Lazy<IPostCategoryRepository> _lazyPostCategoryRepository;
		private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

		public RepositoryManager(RepositoryDbContext dbContext)
		{
			_lazyPostRepository = new Lazy<IPostRepository>(() => new PostRepository(dbContext));
			_lazyCategoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(dbContext));
			_lazyPostCategoryRepository = new Lazy<IPostCategoryRepository>(() => new PostCategoryRepository(dbContext));
			_lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
		}

		public IPostRepository PostRepository => _lazyPostRepository.Value;

		public ICategoryRepository CategoryRepository => _lazyCategoryRepository.Value;

		public IPostCategoryRepository PostCategoryRepository => _lazyPostCategoryRepository.Value;

		public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
	}
}
