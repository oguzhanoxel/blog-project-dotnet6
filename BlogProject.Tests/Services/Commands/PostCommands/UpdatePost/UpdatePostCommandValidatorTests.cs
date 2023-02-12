using FluentAssertions;
using Services.Commands.PostCommands.UpdatePost;

namespace BlogProject.Tests.Services.Commands.PostCommands.UpdatePost
{
	public class UpdatePostCommandValidatorTests
	{
		private readonly UpdatePostCommandValidator _validator;

		public UpdatePostCommandValidatorTests()
		{
			_validator = new UpdatePostCommandValidator();
		}

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

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Count.Should().BeGreaterThan(0);
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void WhenValidCommandGiven_UpdatePostCommandValidator_ShouldIsValidBeTrue()
		{
			// Arrange
			UpdatePostCommand command = new UpdatePostCommand()
			{
				Title = "Test Update Post Valid Title",
				Text = "Test Update Post Valid Text"
			};

			// Act
			var result = _validator.Validate(command);

			// Assert
			result.Errors.Count.Should().Be(0);
			result.IsValid.Should().BeTrue();
		}
	}
}
