using FluentAssertions;
using Services.Commands.PostCommands.CreatePost;

namespace BlogProject.Tests.Services.Commands.PostCommands.CreatePost;

public class CreatePostCommandValidatorTests
{
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

        CreatePostCommandValidator validator = new CreatePostCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
        result.IsValid.Should().BeFalse();
    }
}
