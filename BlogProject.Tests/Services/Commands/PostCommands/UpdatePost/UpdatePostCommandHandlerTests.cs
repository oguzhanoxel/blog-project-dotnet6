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

		public UpdatePostCommandHandlerTests(CommonTestFixture testFixture)
		{
			_repository = new PostRepository(testFixture.Context);
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

			var businessRules = new PostBusinessRules(_repository);

			UpdatePostCommandHandler handler = new UpdatePostCommandHandler(_repository, businessRules);

			// Act
			var result = handler.Handle(command, default);

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

			var businessRules = new PostBusinessRules(_repository);
			var requestedResult = _repository.GetAsync(post => post.Id == command.Id);

			var clonePost = new Post()
			{
				Id = requestedResult.Result.Id,
				Title = requestedResult.Result.Title,
				Text = requestedResult.Result.Text
			};

			UpdatePostCommandHandler handler = new UpdatePostCommandHandler(_repository, businessRules);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(clonePost.Id);
			result.Result.Title.Should().NotBe(clonePost.Title);
			result.Result.Text.Should().NotBe(clonePost.Text);
		}
	}
}
