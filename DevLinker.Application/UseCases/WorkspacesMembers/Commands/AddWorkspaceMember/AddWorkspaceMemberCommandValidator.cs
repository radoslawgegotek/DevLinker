using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using FluentValidation;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Commands.AddWorkspaceMember
{
	public class AddWorkspaceMemberCommandValidator : AbstractValidator<AddWorkspaceMemberCommand>
	{
		private readonly IWorkspaceMemberRepository _repository;
		private readonly ICurrentUserService _currentUserService;

		public AddWorkspaceMemberCommandValidator(
			IWorkspaceMemberRepository context, 
			ICurrentUserService currentUserService)
		{
			_repository = context;
			_currentUserService = currentUserService;

			RuleFor(model => model)
				.MustAsync(async (model, cancelation) =>
				{
					return await _repository.IsMemberAdmin(model.WorkspaceId, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions")
				.MustAsync(async (model, cancelation) =>
				{
					var isMember = await _repository.IsUserMemberOfWorkspace(model.WorkspaceId, model.UserId);
					return !isMember;
				})
				.WithMessage("The user is already assigned to this workspace");
		}
	}
}
