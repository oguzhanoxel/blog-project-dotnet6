using BlogProject.Tests.TestSetup;
using BlogProject.Tests.TestSetup.Mocks;
using Domain.Entities;
using FluentAssertions;
using Services.Commands.PostCommands.UpdatePost;
using Services.Rules;
using static Services.Commands.PostCommands.UpdatePost.UpdatePostCommand;

namespace BlogProject.Tests.Services.Commands.PostCommands
{
	public class UpdatePostCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly RepositoryMock _repository;

		public UpdatePostCommandTests(CommonTestFixture testFixture)
		{
			_repository = new RepositoryMock(testFixture.Context);
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

			UpdatePostCommandValidator validator = new UpdatePostCommandValidator();

			// Act
			var result = validator.Validate(command);

			// Assert
            result.Errors.Count.Should().BeGreaterThan(0);
            result.IsValid.Should().BeFalse();
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

			var repository = _repository.GetPostRepository().Object;
			var businessRules = new PostBusinessRules(repository);

			UpdatePostCommandHandler handler = new UpdatePostCommandHandler(repository, businessRules);

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

			var repository = _repository.GetPostRepository().Object;
			var businessRules = new PostBusinessRules(repository);
			var requestedResult = repository.GetAsync(post => post.Id == command.Id);

			var clonePost = new Post()
			{
				Id = requestedResult.Result.Id,
				Title = requestedResult.Result.Title,
				Text = requestedResult.Result.Text,
				CreatedDate = requestedResult.Result.CreatedDate
			};

			UpdatePostCommandHandler handler = new UpdatePostCommandHandler(repository, businessRules);

			// Act
			var result = handler.Handle(command, default);

			// Assert
			result.IsCompletedSuccessfully.Should().BeTrue();
			result.Result.Id.Should().Be(clonePost.Id);
			result.Result.CreatedDate.Should().Be(clonePost.CreatedDate);
			result.Result.Title.Should().NotBe(clonePost.Title);
			result.Result.Text.Should().NotBe(clonePost.Text);
		}
	}
}
