using Domain.Exceptions.Abstracts;

namespace Domain.Exceptions
{
	public sealed class PostDoesNotBelongToCategoryException : BadRequestException
	{
		PostDoesNotBelongToCategoryException(int postId, int categoryId) : base($"The post with the identifier {postId} does not belong to the category with the identifier {categoryId}")
		{

		}
	}
}
