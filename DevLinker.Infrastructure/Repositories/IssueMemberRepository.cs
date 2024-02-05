using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.DataModel.Context;

namespace DevLinker.Infrastructure.Repositories
{
	public class IssueMemberRepository : BaseRepository<IssueMember>, IIssueMemberRepository
	{
		public IssueMemberRepository(DevLinkerContext context) : base(context)
		{
		}
	}
}
