namespace Contracts.Dtos.PostDtos
{
	public class PostDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Text { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}