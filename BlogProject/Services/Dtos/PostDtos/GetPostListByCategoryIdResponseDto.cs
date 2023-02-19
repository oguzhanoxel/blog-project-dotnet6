namespace Services.Dtos.PostDtos
{
	public class GetPostListByCategoryIdResponseDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string CategoryTitle { get; set; }
	}
}
