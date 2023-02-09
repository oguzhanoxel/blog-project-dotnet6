using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using Contracts.Dtos.CategoryDtos;
using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;
using static Services.Commands.CategoryCommands.CreateCategory.CreateCategoryCommand;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.CreateCategory
{
	public class CreateCategoryCommandHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly CategoryRepository _repository;

		public CreateCategoryCommandHandlerTests(CommonTestFixture testFixture)
		{
			_repository = new CategoryRepository(testFixture.Context);
		}

		[Fact]
		public void WhenValidCreateCategoryCommandGiven_CreateCategoryCommandHandler_ShouldReturnCategoryDto()
		{
			// Arrange
			CreateCategoryCommand command = new CreateCategoryCommand()
			{
				Title = "Created Test Category Title"
			};

			CreateCategoryCommandHandler handler = new CreateCategoryCommandHandler(_repository);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<CategoryDto>();
			result.Result.Title.Should().Be(command.Title);
		}
	}
}
