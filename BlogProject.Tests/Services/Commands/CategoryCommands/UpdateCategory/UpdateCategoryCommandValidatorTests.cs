using BlogProject.Tests.TestSetup;
using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;
using Services.Commands.CategoryCommands.UpdateCategory;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.UpdateCategory
{
	public class UpdateCategoryCommandValidatorTests
	{
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
	}
}
