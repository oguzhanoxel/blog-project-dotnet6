namespace Core.Services.Pipelines.Authorization
{
	public interface ISecuredRequest
	{
		public string[] Roles { get; }
	}
}
