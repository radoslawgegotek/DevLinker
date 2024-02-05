using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;

namespace DevLinker.Domain.IQueries
{
	public interface IIssueQuery
	{
		Task<Page<IssueDto>> GetIssuesAsync(PageProperties properties, int workspaceId);
	}
}
