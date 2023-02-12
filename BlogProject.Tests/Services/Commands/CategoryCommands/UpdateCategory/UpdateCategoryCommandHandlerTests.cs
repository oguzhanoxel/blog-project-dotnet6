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
		private readonly CategoryBusinessRules _businessRules;
		private readonly UpdateCategoryCommandHandler _handler;

		public UpdateCategoryCommandHandlerTests(CommonTestFixture testFixture)
		{
			_categoryRepository = new CategoryRepository(testFixture.Context);
			_postCategoryRepository = new PostCategoryRepository(testFixture.Context);
			_businessRules = new CategoryBusinessRules(_categoryRepository, _postCategoryRepository);
			_handler = new UpdateCategoryCommandHandler(_categoryRepository, _businessRules);
		}

		[Fact]
		public void WhenNonExistCategoryIdGiven_UpdateCategoryCommandHandler_ShouldReturnException()
		{
			// Arrange
			UpdateCategoryCommand command = new UpdateCategoryCommand()
			{
				Id = 0,
				Title = "Update Test Title"
			};

			// Act
			var result = _handler.Handle(command, default);

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
				Title = "Update Test Title"
			};

			var requestedResult = _categoryRepository.GetAsync(post => post.Id == command.Id);

			// Act
			var result = _handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(requestedResult.Result.Id);
			result.Result.Title.Should().Be(requestedResult.Result.Title);
		}
	}
}
