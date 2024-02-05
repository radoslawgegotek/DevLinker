using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using DevLinker.Infrastructure.Repositories;
using FluentValidation;

namespace DevLinker.Application.UseCases.IssuesMembers.Commands.DeleteIssueMember
{
	public class DeleteIssueMemberCommandValidator : AbstractValidator<DeleteIssueMemberCommand>
	{
        private readonly IWorkspaceMemberRepository _repository;
		private readonly ICurrentUserService _currentUserService;

		public DeleteIssueMemberCommandValidator(
			IWorkspaceMemberRepository repository, 
			ICurrentUserService currentUserService)
		{
			_repository = repository;
			_currentUserService = currentUserService;


			RuleFor(model => model)
				.MustAsync(async (model, cancelation) =>
				{
					return await _repository.IsMemberAdmin(model.WorkspaceId, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions");

		}
	}
}
