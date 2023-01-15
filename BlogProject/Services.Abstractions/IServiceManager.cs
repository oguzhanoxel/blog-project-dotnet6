namespace Services.Abstractions
{
	public interface IServiceManager
	{
		IPostService PostService { get; }
		ICategoryService CategoryService { get; }
		IPostCategoryService PostCategoryService { get; }
	}	
}
