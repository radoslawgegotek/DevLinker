using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using FluentValidation;

namespace DevLinker.Application.UseCases.Workspaces.Commands.UpdateWorkspace
{
	public class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
	{
		private readonly IWorkspaceMemberRepository _repository;
		private readonly ICurrentUserService _currentUserService;

		public UpdateWorkspaceCommandValidator(
			IWorkspaceMemberRepository repository, 
			ICurrentUserService currentUserService)
		{
			_repository = repository;
			_currentUserService = currentUserService;

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

			RuleFor(x => x)
				.MustAsync(async (model, cancellation) =>
				{
					return await _repository.IsMemberAdmin(model.Id, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions");
		}
	}
}
