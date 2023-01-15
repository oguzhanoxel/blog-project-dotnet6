namespace Domain.Exceptions.Abstracts
{
	public abstract class NotFoundException : Exception
	{
		protected NotFoundException(string message) : base(message)
		{

		}
	}
}
