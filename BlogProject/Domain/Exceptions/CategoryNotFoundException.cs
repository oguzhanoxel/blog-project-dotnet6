using Domain.Exceptions.Abstracts;

namespace Domain.Exceptions
{
	public sealed class CategoryNotFoundException : NotFoundException
	{
		public CategoryNotFoundException(int Id) : base($"The category with the identifier {Id} was not found.")
		{

		}
	}
}

