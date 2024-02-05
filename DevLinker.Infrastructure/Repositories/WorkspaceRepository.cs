using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.DataModel.Context;
using Microsoft.EntityFrameworkCore;

namespace DevLinker.Infrastructure.Repositories
{
	public class WorkspaceRepository : BaseRepository<Workspace>, IWorkspaceRepository
	{
		private readonly DevLinkerContext _context;

		public WorkspaceRepository(DevLinkerContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> IsWorkspaceCreatedByUser(int workspaceId, string userId)
		{
			var createdBy = await _context.Workspaces
				.AsNoTracking()
				.Where(x => x.Id == workspaceId)
				.Select(x => x.CreatedBy)
				.FirstOrDefaultAsync();
			return createdBy == userId;
		}
	}
}
