using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Contracts.Dtos.PostDtos;
using FluentAssertions;
using Services.Commands.PostCommands.CreatePost;
using static Services.Commands.PostCommands.CreatePost.CreatePostCommand;

namespace BlogProject.Tests.Services.Commands.PostCommands;

public class CreatePostCommandTests : IClassFixture<CommonTestFixture>
{
	private readonly RepositoryMock _repository;

	public CreatePostCommandTests(CommonTestFixture testFixture)
    {
        _repository = new RepositoryMock(testFixture.Context);
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

        CreatePostCommandValidator validator = new CreatePostCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void WhenValidCreatePostCommandGiven_CreatePostCommandHandler_ShouldReturnPostDto()
    {
        // Arrange
        CreatePostCommand command = new CreatePostCommand(){
            Title = "Praesent ac dignissim risus, et rhoncus libero.",
            Text = "Praesent ac dignissim risus, et rhoncus libero. Donec euismod viverra leo quis pretium. Ut condimentum urna vitae rutrum ullamcorper."
        };

        var repository = _repository.GetPostRepository().Object;
        
        CreatePostCommandHandler handler = new CreatePostCommandHandler(repository);

        // Act
        var result = handler.Handle(command, default);

        // Assert
        result.IsCompletedSuccessfully.Should().BeTrue();
        result.Result.Should().BeOfType<PostDto>();
        result.Result.Title.Should().Be(command.Title);
        result.Result.Text.Should().Be(command.Text);
    }
}
