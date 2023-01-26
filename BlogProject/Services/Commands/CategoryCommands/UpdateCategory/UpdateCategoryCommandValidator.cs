using FluentValidation;
using Services.Commands.CategoryCommands.UpdateCategory;

namespace Services.Commands.CategoryCommands.CreateCategory
{
	public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
	{
		public UpdateCategoryCommandValidator()
		{
			RuleFor(c => c.Title)
				.NotNull()
				.NotEmpty()
				.MinimumLength(10)
				.MaximumLength(75);

			RuleFor(p => p.Description)
				.NotNull()
				.NotEmpty();
		}
	}
}
