using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using Contracts.Dtos.PostDtos;
using FluentAssertions;
using Services.Queries.PostQueries.GetPostList;
using static Services.Queries.PostQueries.GetPostList.GetPostListQuery;

namespace BlogProject.Tests.Services.Queries.PostQueries.GetPostList
{
	public class GetPostListQueryHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly PostRepository _repository;
		private readonly GetPostListQueryHandler _handler;

		public GetPostListQueryHandlerTests(CommonTestFixture testFixture)
		{
			_repository = new PostRepository(testFixture.Context);
			_handler = new GetPostListQueryHandler(_repository);
		}

		[Fact]
		public void WhenValidGetPostListQueryGiven_GetListPostQueryHandler_ShouldReturnPostListDto()
		{
			// Arrange
			GetPostListQuery query = new GetPostListQuery();

			// Act
			var result = _handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<List<PostDto>>();
		}
	}
}
