using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLinker.Application.UseCases.IssuesMembers.Commands.AddIssueMember
{
	public class AddIssueMemberCommandValidator : AbstractValidator<AddIssueMemberCommand>
	{
        private readonly IIssueRepository _issueRepository;
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;
		private readonly ICurrentUserService _currentUserService;

		public AddIssueMemberCommandValidator(
			IIssueRepository issueRepository,
			IWorkspaceMemberRepository workspaceMemberRepository,
			ICurrentUserService currentUserService)
		{
			_issueRepository = issueRepository;
			_workspaceMemberRepository = workspaceMemberRepository;
			_currentUserService = currentUserService;

			RuleFor(model => model)
				.MustAsync(async (model, cancelation) =>
				{
					return await _workspaceMemberRepository.IsMemberAdmin(model.WorkspaceId, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions")
				.MustAsync(async (model, cancelation) =>
				{
					return await _workspaceMemberRepository.IsUserMemberOfWorkspace(model.WorkspaceId, model.UserId);
				})
				.WithMessage("User does not belong to Workspace")
				.MustAsync(async (model, cancelation) =>
				{
					return await _issueRepository.IsIssueOfWorkspace(model.IssueId, model.WorkspaceId);
				})
				.WithMessage("Issue does not belong to Workspace");
		}
	}
}
