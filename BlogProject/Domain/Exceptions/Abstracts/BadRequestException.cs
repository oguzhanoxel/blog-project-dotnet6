namespace Domain.Exceptions.Abstracts
{
	public abstract class BadRequestException : Exception
	{
		protected BadRequestException(string message) : base(message)
		{

		}
	}
}
