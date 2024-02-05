using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using FluentValidation;

namespace DevLinker.Application.UseCases.IssuesMembers.Queries.GetIssuesMembers
{
	public class GetIssueMembersQueryValidator : AbstractValidator<GetIssueMembersQuery>
	{
		private readonly IIssueRepository _issueRepository;
		private readonly IWorkspaceMemberRepository _workspaceMemberRepository;
		private readonly ICurrentUserService _currentUserService;

		public GetIssueMembersQueryValidator(
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
					return await _workspaceMemberRepository.IsUserMemberOfWorkspace(model.WorkspaceId, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions")
				.MustAsync(async (model, cancelation) =>
				{
					return await _issueRepository.IsIssueOfWorkspace(model.IssueId, model.WorkspaceId);
				})
				.WithMessage("Issue does not belong to Workspace");

			RuleFor(x => x.OrderBy)
				.Must((orderBy) =>
				{
					return typeof(AppUser).GetProperties()
					.Any(prop => prop.Name == orderBy);
				});
		}
	}
}
