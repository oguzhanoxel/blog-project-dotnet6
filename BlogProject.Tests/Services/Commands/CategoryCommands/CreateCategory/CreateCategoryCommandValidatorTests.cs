using FluentAssertions;
using Services.Commands.CategoryCommands.CreateCategory;

namespace BlogProject.Tests.Services.Commands.CategoryCommands.CreateCategory
{
	public class CreateCategoryCommandValidatorTests
	{
		[Theory]
		[InlineData("abc", "")]
		[InlineData("", "")]
		[InlineData(null, null)]
		public void WhenInvalidCommandGiven_CreateCategoryCommandValidator_ShouldReturnErrors(string title, string description)
		{
			// Arrange
			CreateCategoryCommand command = new CreateCategoryCommand()
			{
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
	}
}
