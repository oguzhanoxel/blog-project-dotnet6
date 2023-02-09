using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using BlogProject.Tests.TestSetup.TestDb;
using Contracts.Dtos.CategoryDtos;
using FluentAssertions;
using Services.Queries.CategoryQueries.GetCategoryById;
using Services.Rules;
using static Services.Queries.CategoryQueries.GetCategoryById.GetCategoryByIdQuery;

namespace BlogProject.Tests.Services.Queries.CategoryQueries.GetCategoryById
{
	public class GetCategoryByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly CategoryRepository _categoryRepository;
		private PostCategoryRepository _postCategoryRepository;

		public GetCategoryByIdQueryHandlerTests(CommonTestFixture testFixture)
		{
			_categoryRepository = new CategoryRepository(testFixture.Context);
			_postCategoryRepository = new PostCategoryRepository(testFixture.Context);
		}

		[Fact]
		public void WhenNonExistCategoryIdGiven_GetCategoryByIdQueryHandler_ShouldReturnException()
		{
			// Arrange
			GetCategoryByIdQuery query = new GetCategoryByIdQuery()
			{
				Id = 0
			};

			var businessRules = new CategoryBusinessRules(_categoryRepository, _postCategoryRepository);
			var requestedResult = _categoryRepository.GetAsync(category => category.Id == query.Id);

			GetCategoryByIdQueryHandler handler = new GetCategoryByIdQueryHandler(_categoryRepository, businessRules);

			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeFalse();
			result.Exception.Should().NotBeNull();
		}

		[Fact]
		public void WhenValidGetCategoryByIdQueryGiven_GetCategoryByIdQueryHandler_ShouldReturnCategoryDto()
		{
			// Arrange
			GetCategoryByIdQuery query = new GetCategoryByIdQuery()
			{
				Id = 2
			};

			var businessRules = new CategoryBusinessRules(_categoryRepository, _postCategoryRepository);
			var requestedResult = _categoryRepository.GetAsync(category => category.Id == query.Id);

			GetCategoryByIdQueryHandler handler = new GetCategoryByIdQueryHandler(_categoryRepository, businessRules);

			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<CategoryDto>();
			result.Result.Title.Should().Be(requestedResult.Result.Title);
		}
	}
}
