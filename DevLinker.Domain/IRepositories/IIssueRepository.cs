using DevLinker.Domain.Entities;

namespace DevLinker.Domain.IRepositories
{
	public interface IIssueRepository : IBaseRepository<Issue>
	{
		Task<bool> IsIssueOfWorkspace(int issueId, int workspaceId);
	}
}
