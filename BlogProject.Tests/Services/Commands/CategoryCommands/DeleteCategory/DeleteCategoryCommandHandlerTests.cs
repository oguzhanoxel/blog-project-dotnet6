using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using FluentAssertions;
using Services.Commands.CategoryCommands.DeleteCategory;
using Services.Rules;
using static Services.Commands.CategoryCommands.DeleteCategory.DeleteCategoryCommand;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.DeleteCategory
{
	public class DeleteCategoryCommandHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly CategoryRepository _categoryRepository;
		private readonly PostCategoryRepository _postCategoryRepository;
		private readonly CategoryBusinessRules _businessRules;
		private readonly DeleteCategoryCommandHandler _handler;

		public DeleteCategoryCommandHandlerTests(CommonTestFixture testFixture)
		{
			_categoryRepository = new CategoryRepository(testFixture.Context);
			_postCategoryRepository = new PostCategoryRepository(testFixture.Context);
			_businessRules = new CategoryBusinessRules(_categoryRepository, _postCategoryRepository);
			_handler = new DeleteCategoryCommandHandler(_categoryRepository, _businessRules);
		}

		[Fact]
		public void WhenCategoryHasPost_DeleteCategoryCommandHandler_ShouldReturnException()
		{
			// Arrange
			DeleteCategoryCommand command = new DeleteCategoryCommand()
			{
				Id = 1
			};

			var requestedResult = _categoryRepository.GetAsync(category => category.Id == command.Id);

			// Act
			var result = _handler.Handle(command, CancellationToken.None);

			// Assert
			result.IsCompletedSuccessfully.Should().BeFalse();
			result.Exception.Should().NotBeNull();
		}

		[Fact]
		public void WhenNonExistCategoryIdGiven_DeleteCategoryCommandHandler_ShouldReturnException()
		{
			// Arrange
			DeleteCategoryCommand command = new DeleteCategoryCommand()
			{
				Id = 0
			};

			// Act
			var result = _handler.Handle(command, CancellationToken.None);

			// Assert
			result.IsCompletedSuccessfully.Should().BeFalse();
			result.Exception.Should().NotBeNull();
		}

		[Fact]
		public void WhenValidDeleteCategoryCommandGiven_DeleteCategoryCommandHandler_ShouldReturnCategoryDto()
		{
			// Arrange
			DeleteCategoryCommand command = new DeleteCategoryCommand()
			{
				Id = 3 // has no post
			};

			var requestedResult = _categoryRepository.GetAsync(category => category.Id == command.Id);

			// Act
			var result = _handler.Handle(command, CancellationToken.None);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(requestedResult.Result.Id);
			result.Result.Title.Should().Be(requestedResult.Result.Title);

			_categoryRepository.GetAsync(category => category.Id == command.Id).Result.Should().BeNull();
		}
	}
}
