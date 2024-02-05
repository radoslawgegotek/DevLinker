using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using MediatR;

namespace DevLinker.Application.UseCases.IssuesMembers.Queries.GetIssuesMembers
{
	public class GetIssueMembersQuery : PageProperties, IRequest<Result<Page<MemberDto>>>
	{
		public int IssueId { get; set; }
		public int WorkspaceId { get; set; }
	}
}
