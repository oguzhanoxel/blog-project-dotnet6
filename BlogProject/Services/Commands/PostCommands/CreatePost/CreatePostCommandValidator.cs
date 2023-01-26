using FluentValidation;

namespace Services.Commands.PostCommands.CreatePost
{
	public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
	{
		public CreatePostCommandValidator()
		{
			RuleFor(p => p.Title)
				.NotNull()
				.NotEmpty()
				.MinimumLength(10)
				.MaximumLength(75);
				
			RuleFor(p => p.Text)
				.NotNull()
				.NotEmpty();
		}
	}
}
