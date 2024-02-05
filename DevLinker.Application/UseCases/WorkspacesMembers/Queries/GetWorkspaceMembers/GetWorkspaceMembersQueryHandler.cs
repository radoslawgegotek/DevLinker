using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.IQueries;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Queries.GetWorkspaceMembers
{
	public class GetWorkspaceMembersQueryHandler : IRequestHandler<GetWorkspaceMembersQuery, Result<Page<MemberDto>>>
	{
		private readonly IWorkspaceMemberQuery _query;
		private readonly IValidator<GetWorkspaceMembersQuery> _validator;

		public GetWorkspaceMembersQueryHandler(
			IWorkspaceMemberQuery query, 
			IValidator<GetWorkspaceMembersQuery> validator)
		{
			_query = query;
			_validator = validator;
		}

		public async Task<Result<Page<MemberDto>>> Handle(GetWorkspaceMembersQuery request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if (validationResult.IsValid)
			{
				var dto = await _query.GetWorkspaceMembers(request, request.WorkspaceId);
				return Result.Ok(dto);
			}
			return Result.Fail<Page<MemberDto>>(validationResult.ToDictionary());
		}
	}
}
