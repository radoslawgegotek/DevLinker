using DevLinker.Domain.Dto;

namespace DevLinker.Domain.IQueries
{
	public interface IWorkspaceQuery
	{
		Task<List<UserWorkspacesDto>> GetUserWorkspaces(string userId);
	}
}
