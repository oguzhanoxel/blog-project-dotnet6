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

		public Mock<ICategoryRepository> GetCategoryRepository()
		{
			Mock<ICategoryRepository> repository = new Mock<ICategoryRepository>();

			repository
				.Setup(r => r.CreateAsync(It.IsAny<Category>()))
				.ReturnsAsync((Category category) =>
				{
					_context.Categories.Add(category);
					_context.SaveChanges();
					return category;
				});

			repository
				.Setup(r => r.DeleteAsync(It.IsAny<Category>()))
				.ReturnsAsync((Category category) =>
				{
					_context.Categories.Remove(category);
					_context.SaveChanges();
					return category;
				});

			repository
				.Setup(r => r.UpdateAsync(It.IsAny<Category>()))
				.ReturnsAsync((Category category) =>
				{
					_context.Categories.Update(category);
					_context.SaveChanges();
					return category;
				});

			repository
				.Setup(r => r.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken) =>
				{
					var category = _context.Categories;
					if (predicate is not null) category.Where(predicate);
					return category.ToList();
				});

			repository
				.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken) =>
				{
					var category = _context.Categories.FirstOrDefault(predicate);
					return category;
				});

			return repository;
		}

		public Mock<IPostCategoryRepository> GetPostCategoryRepository()
		{
			Mock<IPostCategoryRepository> repository = new Mock<IPostCategoryRepository>();

			repository
				.Setup(r => r.CreateAsync(It.IsAny<PostCategory>()))
				.ReturnsAsync((PostCategory postCategory) =>
				{
					_context.PostCategories.Add(postCategory);
					_context.SaveChanges();
					return postCategory;
				});

			repository
				.Setup(r => r.DeleteAsync(It.IsAny<PostCategory>()))
				.ReturnsAsync((PostCategory postCategory) =>
				{
					_context.PostCategories.Remove(postCategory);
					_context.SaveChanges();
					return postCategory;
				});

			repository
				.Setup(r => r.UpdateAsync(It.IsAny<PostCategory>()))
				.ReturnsAsync((PostCategory postCategory) =>
				{
					_context.PostCategories.Update(postCategory);
					_context.SaveChanges();
					return postCategory;
				});

			repository
				.Setup(r => r.GetListAsync(It.IsAny<Expression<Func<PostCategory, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<PostCategory, bool>> predicate, CancellationToken cancellationToken) =>
				{
					var postCategory = _context.PostCategories;
					if (predicate is not null) postCategory.Where(predicate);
					return postCategory.ToList();
				});

			repository
				.Setup(r => r.GetAsync(It.IsAny<Expression<Func<PostCategory, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<PostCategory, bool>> predicate, CancellationToken cancellationToken) =>
				{
					var postCategory = _context.PostCategories.FirstOrDefault(predicate);
					return postCategory;
				});

			return repository;
		}
	}
}
