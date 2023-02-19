using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;
using Services.Dtos.CategoryDtos;
using static Services.Commands.CategoryCommands.CreateCategory.CreateCategoryCommand;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.CreateCategory
{
	public class CreateCategoryCommandHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly CategoryRepository _repository;
		private readonly CreateCategoryCommandHandler _handler;

		public CreateCategoryCommandHandlerTests(CommonTestFixture testFixture)
		{
			_repository = new CategoryRepository(testFixture.Context);
			_handler = new CreateCategoryCommandHandler(_repository);
		}

		[Fact]
		public void WhenValidCreateCategoryCommandGiven_CreateCategoryCommandHandler_ShouldReturnCategoryDto()
		{
			// Arrange
			CreateCategoryCommand command = new CreateCategoryCommand()
			{
				Title = "Created Test Category Title"
			};

			// Act
			var result = _handler.Handle(command, CancellationToken.None);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Should().BeOfType<CategoryResponseDto>();
			result.Result.Title.Should().Be(command.Title);
		}
	}
}
