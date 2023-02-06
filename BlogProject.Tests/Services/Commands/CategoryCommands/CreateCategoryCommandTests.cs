using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Contracts.Dtos.CategoryDtos;
using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;
using static Services.Commands.CategoryCommands.CreateCategory.CreateCategoryCommand;

namespace BlogProject.Tests.Services.Commands.CategoryCommands
{
	public class CreateCategoryCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public CreateCategoryCommandTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
		}

		[Theory]
    	[InlineData("abc", "")]
    	[InlineData("", "")]
    	[InlineData(null, null)]
		public void WhenInvalidCommandGiven_CreateCategoryCommandValidator_ShouldReturnErrors(string title, string description)
		{
			// Arrange
			CreateCategoryCommand command = new CreateCategoryCommand(){
				Title = title,
				Description = description
			};

			CreateCategoryCommandValidator validator = new CreateCategoryCommandValidator();

			// Act
			var result = validator.Validate(command);

			// Assert
			result.Errors.Count.Should().BeGreaterThan(0);
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void WhenValidCreateCategoryCommandGiven_CreateCategoryCommandHandler_ShouldReturnCategoryDto()
		{
			// Arrange
			CreateCategoryCommand command = new CreateCategoryCommand(){
				Title = "Created Test Category Title",
				Description = "created test category description"
			};

			var repository = _repository.GetCategoryRepository().Object;

			CreateCategoryCommandHandler handler = new CreateCategoryCommandHandler(repository);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<CategoryDto>();
			result.Result.Title.Should().Be(command.Title);
			result.Result.Description.Should().Be(command.Description);
		}
	}
}
