using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Domain.Entities;
using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;
using Services.Commands.CategoryCommands.UpdateCategory;
using Services.Rules;
using static Services.Commands.CategoryCommands.UpdateCategory.UpdateCategoryCommand;

namespace BlogProject.Tests.Services.Commands.CategoryCommands
{
	public class UpdateCategoryCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public UpdateCategoryCommandTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
		}

		[Theory]
		[InlineData("abc", "")]
		[InlineData("", "")]
		[InlineData(null, null)]
		public void WhenInvalidCommandGiven_UpdateCategoryCommandValidator_ShouldReturnErrors(string title, string description)
		{
			// Arrange
			UpdateCategoryCommand command = new UpdateCategoryCommand()
			{
				Title = title,
				Description = description
			};

			UpdateCategoryCommandValidator validator = new UpdateCategoryCommandValidator();

			// Act
			var result = validator.Validate(command);

			// Assert
			result.Errors.Count.Should().BeGreaterThan(0);
			result.IsValid.Should().BeFalse();
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

			var categoryRepository = _repository.GetCategoryRepository().Object;
			var postCategoryRepository = _repository.GetPostCategoryRepository().Object;

			var businessRules = new CategoryBusinessRules(categoryRepository, postCategoryRepository);

			UpdateCategoryCommandHandler handler = new UpdateCategoryCommandHandler(categoryRepository, businessRules);

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

			var repositoryCategory = _repository.GetCategoryRepository().Object;
			var repositoryPostCategory = _repository.GetPostCategoryRepository().Object;
			var businessRules = new CategoryBusinessRules(repositoryCategory, repositoryPostCategory);
			var requestedResult = repositoryCategory.GetAsync(post => post.Id == command.Id);

			var cloneCategory = new Category()
			{
				Id = requestedResult.Result.Id,
				Title = requestedResult.Result.Title,
				Description = requestedResult.Result.Description,
			};

			UpdateCategoryCommandHandler handler = new UpdateCategoryCommandHandler(repositoryCategory, businessRules);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(cloneCategory.Id);
			result.Result.Title.Should().NotBe(cloneCategory.Title);
			result.Result.Description.Should().NotBe(cloneCategory.Description);
		}
	}
}
