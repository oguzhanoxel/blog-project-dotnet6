using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using Contracts.Dtos.PostDtos;
using FluentAssertions;
using Services.Commands.PostCommands.CreatePost;
using static Services.Commands.PostCommands.CreatePost.CreatePostCommand;

namespace BlogProject.Tests.Services.Commands.PostCommands.CreatePost;

public class CreatePostCommandHandlerTests : IClassFixture<CommonTestFixture>
{
	private readonly PostRepository _repository;

	public CreatePostCommandHandlerTests(CommonTestFixture testFixture)
    {
        _repository = new PostRepository(testFixture.Context);
    }

    [Fact]
    public void WhenValidCreatePostCommandGiven_CreatePostCommandHandler_ShouldReturnPostDto()
    {
        // Arrange
        CreatePostCommand command = new CreatePostCommand(){
            Title = "Praesent ac dignissim risus, et rhoncus libero.",
            Text = "Praesent ac dignissim risus, et rhoncus libero. Donec euismod viverra leo quis pretium. Ut condimentum urna vitae rutrum ullamcorper."
        };

        CreatePostCommandHandler handler = new CreatePostCommandHandler(_repository);

        // Act
        var result = handler.Handle(command, default);

        // Assert
        result.IsCompletedSuccessfully.Should().BeTrue();
        result.Result.Should().BeOfType<PostDto>();
        result.Result.Title.Should().Be(command.Title);
        result.Result.Text.Should().Be(command.Text);
    }
}
