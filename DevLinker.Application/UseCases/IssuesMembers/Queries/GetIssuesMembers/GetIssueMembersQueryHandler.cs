using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.IQueries;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.IssuesMembers.Queries.GetIssuesMembers
{
	public class GetIssueMembersQueryHandler : IRequestHandler<GetIssueMembersQuery, Result<Page<MemberDto>>>
	{
		private readonly IIssueMemberQuery _query;
		private readonly IValidator<GetIssueMembersQuery> _validator;

		public GetIssueMembersQueryHandler(
			IIssueMemberQuery query, 
			IValidator<GetIssueMembersQuery> validator)
		{
			_query = query;
			_validator = validator;
		}

		public async Task<Result<Page<MemberDto>>> Handle(GetIssueMembersQuery request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if (validationResult.IsValid)
			{
				var dto = await _query.GetIssueMembers(request, request.IssueId);
				return Result.Ok(dto);
			}
			return Result.Fail<Page<MemberDto>>(validationResult.ToDictionary());
		}
	}
}
