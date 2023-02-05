using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Contracts.Dtos.PostDtos;
using FluentAssertions;
using Services.Queries.PostQueries.GetPostById;
using Services.Rules;
using static Services.Queries.PostQueries.GetPostById.GetPostByIdQuery;

namespace BlogProject.Tests.Services.Queries.PostQueries
{
	public class GetPostByIdQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public GetPostByIdQueryTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
		}

				[Fact]
		public void WhenNonExistPostIdGiven_GetPostByIdQueryHandler_ShouldReturnException()
		{
			// Arrange
			GetPostByIdQuery query = new GetPostByIdQuery()
			{
				Id = 0
			};
			
			var repository = _repository.GetPostRepository().Object;
			var businessRules = new PostBusinessRules(repository);
			var requestedResult = repository.GetAsync(post => post.Id == query.Id);

			GetPostByIdQueryHandler handler = new GetPostByIdQueryHandler(repository, businessRules);

			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeFalse();
			result.Exception.Should().NotBeNull();
		}

		[Fact]
		public void WhenValidGetPostByIdQueryGiven_GetPostByIdQueryHandler_ShouldReturnPostDto()
		{
			// Arrange
			GetPostByIdQuery query = new GetPostByIdQuery()
			{
				Id = 2
			};
			
			var repository = _repository.GetPostRepository().Object;
			var businessRules = new PostBusinessRules(repository);
			var requestedResult = repository.GetAsync(post => post.Id == query.Id);

			GetPostByIdQueryHandler handler = new GetPostByIdQueryHandler(repository, businessRules);

			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<PostDto>();
			result.Result.Title.Should().Be(requestedResult.Result.Title);
			result.Result.Text.Should().Be(requestedResult.Result.Text);
		}
	}
}
