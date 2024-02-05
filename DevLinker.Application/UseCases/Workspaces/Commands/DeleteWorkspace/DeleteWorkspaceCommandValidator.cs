using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using FluentValidation;

namespace DevLinker.Application.UseCases.Workspaces.Commands.DeleteWorkspace
{
	public class DeleteWorkspaceCommandValidator : AbstractValidator<DeleteWorkspaceCommand>
	{
        private readonly IWorkspaceRepository _repository;
		private readonly ICurrentUserService _currentUserService;

		public DeleteWorkspaceCommandValidator(
			IWorkspaceRepository repository, 
			ICurrentUserService currentUserService)
		{
			_repository = repository;
			_currentUserService = currentUserService;

			RuleFor(x => x)
				.MustAsync(async (model, cancellation) =>
				{
					return await _repository.IsWorkspaceCreatedByUser(model.Id, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions");
		}
	}
}
