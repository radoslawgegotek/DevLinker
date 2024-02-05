using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using FluentValidation;

namespace DevLinker.Application.UseCases.Issues.Commands.UpdateIssue
{
	public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
	{
		private readonly IWorkspaceMemberRepository _memberRepository;
		private readonly ICurrentUserService _currentUserService;

		public UpdateIssueCommandValidator(
			IWorkspaceMemberRepository memberRepository, 
			ICurrentUserService currentUserService)
		{
			_memberRepository = memberRepository;
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
				.MustAsync(async (model, cancelation) =>
				{
					return await _memberRepository.IsMemberAdmin(model.WorkspaceId, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("The User is not admin of Workspace");
		}
	}
}
