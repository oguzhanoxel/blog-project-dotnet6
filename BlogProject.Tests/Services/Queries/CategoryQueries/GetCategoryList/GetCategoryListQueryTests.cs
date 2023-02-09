using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using Contracts.Dtos.CategoryDtos;
using FluentAssertions;
using Services.Queries.CategoryQueries.GetCategoryList;
using static Services.Queries.CategoryQueries.GetCategoryList.GetCategoryListQuery;

namespace BlogProject.Tests.Services.Queries.CategoryQueries.GetCategoryList
{
	public class GetCategoryListQueryHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly CategoryRepository _repository;

		public GetCategoryListQueryHandlerTests(CommonTestFixture testFixture)
		{
			_repository = new CategoryRepository(testFixture.Context);
		}

		[Fact]
		public void WhenValidGetCategoryListQueryGiven_GetListCategoryQueryHandler_ShouldReturnCategoryListDto()
		{
			// Arrange
			GetCategoryListQuery query = new GetCategoryListQuery();

			GetCategoryListQueryHandler handler = new GetCategoryListQueryHandler(_repository);

			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<List<CategoryDto>>();
		}
	}
}
