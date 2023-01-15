namespace Domain.Repositories
{
	public interface IRepositoryManager
	{
		IPostRepository PostRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		IPostCategoryRepository PostCategoryRepository { get; }
		IUnitOfWork UnitOfWork { get; }
	}
}
