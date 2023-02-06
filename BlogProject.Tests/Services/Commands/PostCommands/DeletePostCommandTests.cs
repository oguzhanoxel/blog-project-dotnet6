using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using FluentAssertions;
using Services.Commands.PostCommands.DeletePost;
using Services.Rules;
using static Services.Commands.PostCommands.DeletePost.DeletePostCommand;

namespace BlogProject.Tests.Services.Commands.PostCommands;

public class DeletePostCommandTests : IClassFixture<CommonTestFixture>
{
	private readonly RepositoryMock _repository;

	public DeletePostCommandTests(CommonTestFixture testFixture)
    {
        _repository = new RepositoryMock(testFixture.Context);
    }

    [Fact]
    public void WhenNonExistPostIdGiven_DeletePostCommandHandler_ShouldReturnException()
    {
        // Arrange
        DeletePostCommand command = new DeletePostCommand(){
			Id = 0
        };

        var repository = _repository.GetPostRepository().Object;
		var businessRules = new PostBusinessRules(repository);

        DeletePostCommandHandler handler = new DeletePostCommandHandler(repository, businessRules);

        // Act
        var result = handler.Handle(command, default);

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

        var repository = _repository.GetPostRepository().Object;
        
		var businessRules = new PostBusinessRules(repository);
		var requestedResult = repository.GetAsync(post => post.Id == command.Id);

        DeletePostCommandHandler handler = new DeletePostCommandHandler(repository, businessRules);

        // Act
        var result = handler.Handle(command, default);

        // Assert
		result.IsCompletedSuccessfully.Should().BeTrue();
		result.Result.Id.Should().Be(requestedResult.Result.Id);
		result.Result.Title.Should().Be(requestedResult.Result.Title);
		result.Result.Text.Should().Be(requestedResult.Result.Text);

        repository.GetAsync(post => post.Id == command.Id).Result.Should().BeNull();
    }
}
