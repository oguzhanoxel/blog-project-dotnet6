using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.TestDb;
using Domain.Entities;
using FluentAssertions;
using Services.Commands.PostCommands.UpdatePost;
using Services.Rules;
using static Services.Commands.PostCommands.UpdatePost.UpdatePostCommand;

namespace BlogProject.Tests.Services.Commands.PostCommands.UpdatePost
{
	public class UpdatePostCommandHandlerTests : IClassFixture<CommonTestFixture>
	{
		private readonly PostRepository _repository;
		private readonly PostBusinessRules _businessRules;
		private readonly UpdatePostCommandHandler _handler;

		public UpdatePostCommandHandlerTests(CommonTestFixture testFixture)
		{
			_repository = new PostRepository(testFixture.Context);
			_businessRules = new PostBusinessRules(_repository);
			_handler = new UpdatePostCommandHandler(_repository, _businessRules);
		}

		[Fact]
		public void WhenNonExistPostIdGiven_UpdatePostCommandHandler_ShouldReturnException()
		{
			// Arrange
			UpdatePostCommand command = new UpdatePostCommand()
			{
				Id = 0,
				Title = "Update Test Title",
				Text = "Update Test Text"
			};

			// Act
			var result = _handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeFalse();
			result.Exception.Should().NotBeNull();
		}

		[Fact]
		public void WhenValidUpdatePostCommandGiven_UpdatePostCommandHandler_ShouldReturnPostDto()
		{
			// Arrange
			UpdatePostCommand command = new UpdatePostCommand()
			{
				Id = 2,
				Title = "Update Test Title",
				Text = "Update Test Text"
			};

			var requestedResult = _repository.GetAsync(post => post.Id == command.Id);

			// Act
			var result = _handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(requestedResult.Result.Id);
			result.Result.Title.Should().Be(requestedResult.Result.Title);
			result.Result.Text.Should().Be(requestedResult.Result.Text);
		}
	}
}
