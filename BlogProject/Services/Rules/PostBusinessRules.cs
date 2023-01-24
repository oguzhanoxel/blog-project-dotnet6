using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using Services.Constants;

namespace Services.Rules
{
	public class PostBusinessRules
	{
		private readonly IPostRepository _postRepository;

		public PostBusinessRules(IPostRepository postRepository)
		{
			_postRepository = postRepository;
		}

		public async Task PostShouldExistWhenRequested(int id)
		{
			var post = await _postRepository.GetAsync(post => post.Id == id);
			if (post is null) throw new BusinessException(ExceptionConstants.PostNotFound);
		}
	}
}
