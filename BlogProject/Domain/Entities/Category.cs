namespace Domain.Entities
{
	public class Category
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }

		public ICollection<PostCategory>? PostCategories { get; set; }

	}
}
