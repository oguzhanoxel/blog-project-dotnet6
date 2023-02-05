using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Contracts.Dtos.PostDtos;
using FluentAssertions;
using Services.Queries.PostQueries.GetPostList;
using static Services.Queries.PostQueries.GetPostList.GetPostListQuery;

namespace BlogProject.Tests.Services.Queries.PostQueries
{
	public class GetPostListQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public GetPostListQueryTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
		}

		[Fact]
		public void WhenValidGetPostListQueryGiven_GetListPostQueryHandler_ShouldReturnPostListDto()
		{
			// Arrange
			GetPostListQuery query = new GetPostListQuery();

			var repository = _repository.GetPostRepository().Object;

			GetPostListQueryHandler handler = new GetPostListQueryHandler(repository);

			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<List<PostDto>>();
		}
	}
}
