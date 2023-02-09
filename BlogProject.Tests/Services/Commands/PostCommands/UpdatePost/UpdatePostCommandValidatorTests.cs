using FluentAssertions;
using Services.Commands.PostCommands.UpdatePost;

namespace BlogProject.Tests.Services.Commands.PostCommands.UpdatePost
{
	public class UpdatePostCommandValidatorTests
	{
		[Theory]
		[InlineData("abc", "")]
		[InlineData("", "")]
		[InlineData(null, null)]
		public void WhenInvalidCommandGiven_UpdatePostCommandValidator_ShouldReturnErrors(string title, string text)
		{
			// Arrange
			UpdatePostCommand command = new UpdatePostCommand()
			{
				Title = title,
				Text = text
			};

			UpdatePostCommandValidator validator = new UpdatePostCommandValidator();

			// Act
			var result = validator.Validate(command);

			// Assert
			result.Errors.Count.Should().BeGreaterThan(0);
			result.IsValid.Should().BeFalse();
		}
	}
}
