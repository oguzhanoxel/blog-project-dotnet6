using Domain.Exceptions.Abstracts;

namespace Domain.Exceptions
{
	public sealed class PostNotFoundException : NotFoundException
	{
		public PostNotFoundException(int Id) : base($"The post with the identifier {Id} was not found.")
		{

		}
	}
}

