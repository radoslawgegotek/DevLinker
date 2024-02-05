using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;

namespace DevLinker.Domain.IQueries
{
	public interface IWorkspaceMemberQuery
	{
		Task<Page<MemberDto>> GetWorkspaceMembers(PageProperties properties, int workspaceId);
	}
}
