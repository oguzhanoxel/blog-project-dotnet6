using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.CreateCategory
{
	public class CreateCategoryCommandValidatorTests
	{
		private readonly CreateCategoryCommandValidator _validator;

		public CreateCategoryCommandValidatorTests()
		{
			_validator = new CreateCategoryCommandValidator();
		}

		[Theory]
		[InlineData("abc")]
		[InlineData("")]
		[InlineData(null)]
		public void WhenInvalidCommandGiven_CreateCategoryCommandValidator_ShouldReturnErrors(string title)
		{
			// Arrange
			CreateCategoryCommand command = new CreateCategoryCommand()
			{
				Title = title
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Count.Should().BeGreaterThan(0);
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void WhenValidCommandGiven_CreateCategoryCommandValidator_ShouldIsValidBeTrue()
		{
			// Arrange
			CreateCategoryCommand command = new CreateCategoryCommand()
			{
				Title = "Create Category Title Valid"
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Count.Should().Be(0);
			result.IsValid.Should().BeTrue();
		}
	}
}
