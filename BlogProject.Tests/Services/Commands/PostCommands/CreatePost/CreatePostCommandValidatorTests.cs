using FluentAssertions;
using Services.Commands.PostCommands.CreatePost;

namespace BlogProject.Tests.Services.Commands.PostCommands.CreatePost;

public class CreatePostCommandValidatorTests
{
	private readonly CreatePostCommandValidator _validator;

	public CreatePostCommandValidatorTests()
    {
        _validator = new CreatePostCommandValidator();
    }

    [Theory]
    [InlineData("abc", "")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void WhenInvalidCommandGiven_CreatePostCommandValidator_ShouldReturnErrors(string title, string text)
    {
        // Arrange
        CreatePostCommand command = new CreatePostCommand(){
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
    public void WhenValidCommandGiven_CreatePostCommandValidator_ShouldIsValidBeTrue()
    {
        // Arrange
        CreatePostCommand command = new CreatePostCommand(){
            Title = "Valid Create Post Title",
            Text = "Valid Create Post Text"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
        result.IsValid.Should().BeTrue();
    }
}
