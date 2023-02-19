using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using FluentAssertions;
using Services.Commands.PostCommands.CreatePost;
using Services.Dtos.PostDtos;
using static Services.Commands.PostCommands.CreatePost.CreatePostCommand;

namespace BlogProject.Tests.Services.Commands.PostCommands.CreatePost;

public class CreatePostCommandHandlerTests : IClassFixture<CommonTestFixture>
{
	private readonly PostRepository _repository;
	private readonly CreatePostCommandHandler _handler;

	public CreatePostCommandHandlerTests(CommonTestFixture testFixture)
    {
        _repository = new PostRepository(testFixture.Context);
        _handler = new CreatePostCommandHandler(_repository);
    }

    [Fact]
    public void WhenValidCreatePostCommandGiven_CreatePostCommandHandler_ShouldReturnPostDto()
    {
        // Arrange
        CreatePostCommand command = new CreatePostCommand(){
            Title = "Praesent ac dignissim risus, et rhoncus libero.",
            Text = "Praesent ac dignissim risus, et rhoncus libero. Donec euismod viverra leo quis pretium. Ut condimentum urna vitae rutrum ullamcorper."
        };

        // Act
        var result = _handler.Handle(command, default);

        // Assert
        result.IsCompletedSuccessfully.Should().BeTrue();
        result.Result.Should().BeOfType<PostResponseDto>();
        result.Result.Title.Should().Be(command.Title);
        result.Result.Text.Should().Be(command.Text);
    }
}
