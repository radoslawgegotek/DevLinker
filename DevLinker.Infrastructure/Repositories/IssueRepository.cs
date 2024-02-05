using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.DataModel.Context;
using Microsoft.EntityFrameworkCore;

namespace DevLinker.Infrastructure.Repositories
{
	public class IssueRepository : BaseRepository<Issue>, IIssueRepository
	{
		private readonly DevLinkerContext _context;

		public IssueRepository(DevLinkerContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> IsIssueOfWorkspace(int issueId, int workspaceId)
		{
			return workspaceId == await _context.Issues
				.AsNoTracking()
				.Where(x => x.Id == issueId)
				.Select(x => x.WorkspaceId)
				.FirstOrDefaultAsync();
		}
	}
}
