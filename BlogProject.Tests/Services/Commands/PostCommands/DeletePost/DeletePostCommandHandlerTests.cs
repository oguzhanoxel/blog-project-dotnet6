using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using FluentAssertions;
using Services.Commands.PostCommands.DeletePost;
using Services.Rules;
using static Services.Commands.PostCommands.DeletePost.DeletePostCommand;

namespace BlogProject.Tests.Services.Commands.PostCommands.DeletePost;

public class DeletePostCommandHandlerTests : IClassFixture<CommonTestFixture>
{
	private readonly PostRepository _repository;
	private readonly PostBusinessRules _businessRules;
	private readonly DeletePostCommandHandler _handler;

	public DeletePostCommandHandlerTests(CommonTestFixture testFixture)
    {
        _repository = new PostRepository(testFixture.Context);
        _businessRules = new PostBusinessRules(_repository);
        _handler = new DeletePostCommandHandler(_repository, _businessRules);
    }

    [Fact]
    public void WhenNonExistPostIdGiven_DeletePostCommandHandler_ShouldReturnException()
    {
        // Arrange
        DeletePostCommand command = new DeletePostCommand(){
			Id = 0
        };

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
		result.IsCompletedSuccessfully.Should().BeFalse();
        result.Exception.Should().NotBeNull();
    }

    [Fact]
    public void WhenValidDeletePostCommandGiven_DeletePostCommandHandler_ShouldReturnPostDto()
    {
        // Arrange
        DeletePostCommand command = new DeletePostCommand(){
			Id = 2
        };

		var requestedResult = _repository.GetAsync(post => post.Id == command.Id);

        // Act
        var result = _handler.Handle(command, default);

        // Assert
		result.IsCompletedSuccessfully.Should().BeTrue();
		result.Result.Id.Should().Be(requestedResult.Result.Id);
		result.Result.Title.Should().Be(requestedResult.Result.Title);
		result.Result.Text.Should().Be(requestedResult.Result.Text);

        _repository.GetAsync(post => post.Id == command.Id).Result.Should().BeNull();
    }
}
