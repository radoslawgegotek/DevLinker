using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using DevLinker.Infrastructure.Repositories;
using FluentValidation;

namespace DevLinker.Application.UseCases.Issues.Queries.GetIssues
{
    public class GetIssuesQueryValidator : AbstractValidator<GetIssuesQuery>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;

		public GetIssuesQueryValidator(
			ICurrentUserService currentUserService,
			IWorkspaceMemberRepository workspaceMemberRepository)
		{
			_currentUserService = currentUserService;
			_workspaceMemberRepository = workspaceMemberRepository;

			RuleFor(model => model)
				.MustAsync(async (model, cancelation) =>
				{
					return await _workspaceMemberRepository.IsUserMemberOfWorkspace(model.WorkspaceId, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions");

			RuleFor(x => x.OrderBy)
				.Must((orderBy) =>
				{
					return typeof(Issue).GetProperties()
					.Any(prop => prop.Name == orderBy);
				});
		}
	}
}
