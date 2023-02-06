using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Contracts.Dtos.CategoryDtos;
using FluentAssertions;
using Services.Queries.CategoryQueries.GetCategoryById;
using Services.Rules;
using static Services.Queries.CategoryQueries.GetCategoryById.GetCategoryByIdQuery;

namespace BlogProject.Tests.Services.Queries.CategoryQueries
{
	public class GetCategoryByIdQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public GetCategoryByIdQueryTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
		}

		[Fact]
		public void WhenNonExistCategoryIdGiven_GetCategoryByIdQueryHandler_ShouldReturnException()
		{
			// Arrange
			GetCategoryByIdQuery query = new GetCategoryByIdQuery()
			{
				Id = 0
			};

			var repositoryCategory = _repository.GetCategoryRepository().Object;
			var repositoryPostCategory = _repository.GetPostCategoryRepository().Object;
			var businessRules = new CategoryBusinessRules(repositoryCategory, repositoryPostCategory);
			var requestedResult = repositoryCategory.GetAsync(category => category.Id == query.Id);

			GetCategoryByIdQueryHandler handler = new GetCategoryByIdQueryHandler(repositoryCategory, businessRules);

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

			var repositoryCategory = _repository.GetCategoryRepository().Object;
			var repositoryPostCategory = _repository.GetPostCategoryRepository().Object;
			var businessRules = new CategoryBusinessRules(repositoryCategory, repositoryPostCategory);
			var requestedResult = repositoryCategory.GetAsync(category => category.Id == query.Id);

			GetCategoryByIdQueryHandler handler = new GetCategoryByIdQueryHandler(repositoryCategory, businessRules);


			// Act
			var result = handler.Handle(query, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<CategoryDto>();
			result.Result.Title.Should().Be(requestedResult.Result.Title);
			result.Result.Description.Should().Be(requestedResult.Result.Description);
		}
	}
}
