using System.Linq.Expressions;
using BlogProject.Tests.TestSetup.TestDb;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace BlogProject.Tests.TestSetup.Mocks
{
	public class RepositoryMock
	{
		private readonly TestDbContext _context;

		public RepositoryMock(TestDbContext context)
		{
			_context = context;
		}

		public Mock<IPostRepository> GetPostRepository()
		{
			_context.AddRange(
				
			);
			_context.SaveChanges();


			Mock<IPostRepository> repository = new Mock<IPostRepository>();

			repository
				.Setup(r => r.CreateAsync(It.IsAny<Post>()))
				.ReturnsAsync((Post post) =>
				{
					_context.Posts.Add(post);
					_context.SaveChanges();
					return post;
				});

			repository
				.Setup(r => r.DeleteAsync(It.IsAny<Post>()))
				.ReturnsAsync((Post post) =>
				{
					_context.Posts.Remove(post);
					_context.SaveChanges();
					return post;
				});

			repository
				.Setup(r => r.UpdateAsync(It.IsAny<Post>()))
				.ReturnsAsync((Post post) =>
				{
					_context.Posts.Update(post);
					_context.SaveChanges();
					return post;
				});

			repository
				.Setup(r => r.GetListAsync(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<Post, bool>> predicate, CancellationToken cancellationToken) =>
				{
					var post = _context.Posts;
					if (predicate is not null) post.Where(predicate);
					return post.ToList();
				});

			repository
				.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<Post, bool>> predicate, CancellationToken cancellationToken) =>
				{
					var post = _context.Posts.FirstOrDefault(predicate);
					return post;
				});

			return repository;
		}
	}
}
