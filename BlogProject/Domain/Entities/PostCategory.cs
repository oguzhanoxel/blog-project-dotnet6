using Core.Persistence.Repositories;

namespace Domain.Entities
{
	public class PostCategory : Entity
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public virtual Post? Post { get; set; }
		public int CategoryId { get; set; }
		public virtual Category? Category { get; set; }
	}
}
