using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using MediatR;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Queries.GetWorkspaceMembers
{
	public class GetWorkspaceMembersQuery : PageProperties, IRequest<Result<Page<MemberDto>>>
	{
		public int WorkspaceId { get; set; }
	}
}
