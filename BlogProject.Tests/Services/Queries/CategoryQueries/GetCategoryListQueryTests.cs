using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Contracts.Dtos.CategoryDtos;
using FluentAssertions;
using Services.Queries.CategoryQueries.GetCategoryList;
using static Services.Queries.CategoryQueries.GetCategoryList.GetCategoryListQuery;

namespace BlogProject.Tests.Services.Queries.CategoryQueries
{
	public class GetCategoryListQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public GetCategoryListQueryTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
		}

		[Fact]
		public void WhenValidGetCategoryListQueryGiven_GetListCategoryQueryHandler_ShouldReturnCategoryListDto()
		{
			// Arrange
			GetCategoryListQuery query = new GetCategoryListQuery();

			var repository = _repository.GetCategoryRepository().Object;

			GetCategoryListQueryHandler handler = new GetCategoryListQueryHandler(repository);

			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<List<CategoryDto>>();
		}
	}
}
