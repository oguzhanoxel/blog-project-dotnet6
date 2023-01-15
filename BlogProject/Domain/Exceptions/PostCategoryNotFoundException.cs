using Domain.Exceptions.Abstracts;

namespace Domain.Exceptions
{
	public sealed class PostCategoryNotFoundException : NotFoundException
	{
		public PostCategoryNotFoundException(int Id) : base($"The post-category with the identifier {Id} was not found.")
		{

		}
	}
}
