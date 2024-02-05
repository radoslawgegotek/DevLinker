using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;

namespace DevLinker.Domain.IQueries
{
	public interface IIssueMemberQuery
	{
		Task<Page<MemberDto>> GetIssueMembers(PageProperties properties, int issueId);
	}
}
