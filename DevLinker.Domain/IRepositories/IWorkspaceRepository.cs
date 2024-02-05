using DevLinker.Domain.Entities;

namespace DevLinker.Domain.IRepositories
{
	public interface IWorkspaceRepository : IBaseRepository<Workspace>
	{
		public Task<bool> IsWorkspaceCreatedByUser(int workspaceId, string userId);
	}
}
