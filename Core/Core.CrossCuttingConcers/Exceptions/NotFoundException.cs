namespace Core.CrossCuttingConcers.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string message) : base(message)
		{ }
	}
}
