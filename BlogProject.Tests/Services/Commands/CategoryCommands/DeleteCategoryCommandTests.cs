using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using FluentAssertions;
using Services.Commands.CategoryCommands.DeleteCategory;
using Services.Rules;
using static Services.Commands.CategoryCommands.DeleteCategory.DeleteCategoryCommand;

namespace BlogProject.Tests.Services.Commands.CategoryCommands
{
	public class DeleteCategoryCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public DeleteCategoryCommandTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
		}

		[Fact]
		public void WhenCategoryHasPost_DeleteCategoryCommandHandler_ShouldReturnException()
		{
			// Arrange
			DeleteCategoryCommand command = new DeleteCategoryCommand()
			{
				Id = 1
			};

			var categoryRepository = _repository.GetCategoryRepository().Object;
			var postCategoryRepository = _repository.GetPostCategoryRepository().Object;

			var businessRules = new CategoryBusinessRules(categoryRepository, postCategoryRepository);
			var requestedResult = categoryRepository.GetAsync(category => category.Id == command.Id);

			DeleteCategoryCommandHandler handler = new DeleteCategoryCommandHandler(categoryRepository, businessRules);

			// Act
			var result = handler.Handle(command, default);

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

			var categoryRepository = _repository.GetCategoryRepository().Object;
			var postCategoryRepository = _repository.GetPostCategoryRepository().Object;

			var businessRules = new CategoryBusinessRules(categoryRepository, postCategoryRepository);
			var requestedResult = categoryRepository.GetAsync(category => category.Id == command.Id);

			DeleteCategoryCommandHandler handler = new DeleteCategoryCommandHandler(categoryRepository, businessRules);

			// Act
			var result = handler.Handle(command, default);

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

			var categoryRepository = _repository.GetCategoryRepository().Object;
			var postCategoryRepository = _repository.GetPostCategoryRepository().Object;

			var businessRules = new CategoryBusinessRules(categoryRepository, postCategoryRepository);
			var requestedResult = categoryRepository.GetAsync(category => category.Id == command.Id);

			DeleteCategoryCommandHandler handler = new DeleteCategoryCommandHandler(categoryRepository, businessRules);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(requestedResult.Result.Id);
			result.Result.Title.Should().Be(requestedResult.Result.Title);
			result.Result.Description.Should().Be(requestedResult.Result.Description);

			categoryRepository.GetAsync(category => category.Id == command.Id).Result.Should().BeNull();
		}
	}
}
