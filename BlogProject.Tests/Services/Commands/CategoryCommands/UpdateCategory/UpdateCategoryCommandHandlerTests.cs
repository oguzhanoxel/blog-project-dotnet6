using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using Domain.Entities;
using FluentAssertions;
using Services.Commands.CategoryCommands.UpdateCategory;
using Services.Rules;
using static Services.Commands.CategoryCommands.UpdateCategory.UpdateCategoryCommand;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.UpdateCategory
{
	public class UpdateCategoryCommandHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly CategoryRepository _categoryRepository;
		private readonly PostCategoryRepository _postCategoryRepository;

		public UpdateCategoryCommandHandlerTests(CommonTestFixture testFixture)
		{
			_categoryRepository = new CategoryRepository(testFixture.Context);
			_postCategoryRepository = new PostCategoryRepository(testFixture.Context);
		}

		[Fact]
		public void WhenNonExistCategoryIdGiven_UpdateCategoryCommandHandler_ShouldReturnException()
		{
			// Arrange
			UpdateCategoryCommand command = new UpdateCategoryCommand()
			{
				Id = 0,
				Title = "Update Test Title",
				Description = "Update Test Description"
			};

			var businessRules = new CategoryBusinessRules(_categoryRepository, _postCategoryRepository);

			UpdateCategoryCommandHandler handler = new UpdateCategoryCommandHandler(_categoryRepository, businessRules);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeFalse();
			result.Exception.Should().NotBeNull();
		}

		[Fact]
		public void WhenValidUpdateCategoryCommandGiven_UpdateCategoryCommandHandler_ShouldReturnCategoryDto()
		{
			// Arrange
			UpdateCategoryCommand command = new UpdateCategoryCommand()
			{
				Id = 2,
				Title = "Update Test Title",
				Description = "Update Test Description"
			};

			var businessRules = new CategoryBusinessRules(_categoryRepository, _postCategoryRepository);
			var requestedResult = _categoryRepository.GetAsync(post => post.Id == command.Id);

			var cloneCategory = new Category()
			{
				Id = requestedResult.Result.Id,
				Title = requestedResult.Result.Title,
			};

			UpdateCategoryCommandHandler handler = new UpdateCategoryCommandHandler(_categoryRepository, businessRules);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(cloneCategory.Id);
			result.Result.Title.Should().NotBe(cloneCategory.Title);
		}
	}
}
