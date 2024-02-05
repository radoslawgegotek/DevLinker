using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using DevLinker.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Commands.DeleteWorkspaceMember
{
	public class DeleteWorkspaceMemberCommandValidator : AbstractValidator<DeleteWorkspaceMemberCommand>
	{
		private readonly IWorkspaceMemberRepository _repository;
		private readonly ICurrentUserService _currentUserService;

		public DeleteWorkspaceMemberCommandValidator(
			IWorkspaceMemberRepository repository, 
			ICurrentUserService currentUserService)
		{
			_repository = repository;
			_currentUserService = currentUserService;

			RuleFor(x => x.WorkspaceMemberId)
				.NotEmpty()
				.NotNull();

			RuleFor(model => model)
				.MustAsync(async (model, cancelation) =>
				{
					return await _repository.IsMemberAdmin(model.WorkspaceMemberId, _currentUserService.GetCurrnetUserId());
				})
				.WithMessage("Wrong permissions");
		}
	}
}
