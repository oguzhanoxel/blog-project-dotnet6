using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using Contracts.Dtos.PostDtos;
using FluentAssertions;
using Services.Queries.PostQueries.GetPostById;
using Services.Rules;
using static Services.Queries.PostQueries.GetPostById.GetPostByIdQuery;

namespace BlogProject.Tests.Services.Queries.PostQueries.GetPostById
{
	public class GetPostByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly PostRepository _repository;
		private readonly PostBusinessRules _businessRules;
		private readonly GetPostByIdQueryHandler _handler;

		public GetPostByIdQueryHandlerTests(CommonTestFixture testFixture)
		{
			_repository = new PostRepository(testFixture.Context);
			_businessRules = new PostBusinessRules(_repository);
			_handler = new GetPostByIdQueryHandler(_repository, _businessRules);
		}

		[Fact]
		public void WhenNonExistPostIdGiven_GetPostByIdQueryHandler_ShouldReturnException()
		{
			// Arrange
			GetPostByIdQuery query = new GetPostByIdQuery()
			{
				Id = 0
			};

			// Act
			var result = _handler.Handle(query, default);

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

			var requestedResult = _repository.GetAsync(post => post.Id == query.Id);

			// Act
			var result = _handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<PostDto>();
			result.Result.Title.Should().Be(requestedResult.Result.Title);
			result.Result.Text.Should().Be(requestedResult.Result.Text);
		}
	}
}
