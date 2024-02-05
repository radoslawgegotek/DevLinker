using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using DevLinker.Infrastructure.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Queries.GetWorkspaceMembers
{
	public class GetWorkspaceMembersQueryValidator : AbstractValidator<GetWorkspaceMembersQuery>
	{
        private readonly ICurrentUserService _currentUserService;
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;

		public GetWorkspaceMembersQueryValidator(
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
					return typeof(AppUser).GetProperties()
					.Any(prop => prop.Name == orderBy);
				});
		}
	}
}
