using DevLinker.Domain.Entities;

namespace DevLinker.Domain.IRepositories
{
	public interface IWorkspaceMemberRepository : IBaseRepository<WorkspaceMember>
	{
		Task<bool> IsUserMemberOfWorkspace(int workspaceId, string userId);
		Task<bool> IsMemberAdmin(int workspaceId, string userId);
	}
}
