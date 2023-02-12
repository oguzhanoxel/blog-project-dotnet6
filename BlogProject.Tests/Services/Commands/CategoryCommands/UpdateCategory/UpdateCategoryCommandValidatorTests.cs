using BlogProject.Tests.TestSetup;
using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;
using Services.Commands.CategoryCommands.UpdateCategory;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.UpdateCategory
{
	public class UpdateCategoryCommandValidatorTests
	{
		private readonly UpdateCategoryCommandValidator _validator;

		public UpdateCategoryCommandValidatorTests()
		{
			_validator = new UpdateCategoryCommandValidator();
		}

		[Theory]
		[InlineData("abc")]
		[InlineData("")]
		[InlineData(null)]
		public void WhenInvalidCommandGiven_UpdateCategoryCommandValidator_ShouldReturnErrors(string title)
		{
			// Arrange
			UpdateCategoryCommand command = new UpdateCategoryCommand()
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
		public void WhenValidCommandGiven_UpdateCategoryCommandValidator_ShouldIsValidBeTrue()
		{
			// Arrange
			UpdateCategoryCommand command = new UpdateCategoryCommand()
			{
				Title = "Update Test Title Valid"
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Count.Should().Be(0);
			result.IsValid.Should().BeTrue();
		}
	}
}
