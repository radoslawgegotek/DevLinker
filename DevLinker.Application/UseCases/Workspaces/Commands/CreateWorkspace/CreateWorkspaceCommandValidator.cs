using FluentValidation;

namespace DevLinker.Application.UseCases.Workspaces.Commands.CreateWorkspace
{
	public class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceCommand>
	{
        public CreateWorkspaceCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(255);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(1024);
        }
    }
}
