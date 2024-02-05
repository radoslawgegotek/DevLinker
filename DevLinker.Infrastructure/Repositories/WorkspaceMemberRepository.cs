using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.DataModel.Context;
using Microsoft.EntityFrameworkCore;

namespace DevLinker.Infrastructure.Repositories
{
	public class WorkspaceMemberRepository : BaseRepository<WorkspaceMember>, IWorkspaceMemberRepository
	{
		private readonly DevLinkerContext _context;

		public WorkspaceMemberRepository(DevLinkerContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> IsUserMemberOfWorkspace(int workspaceId, string userId)
		{
			var res = await _context.WorkspaceMembers
				.AsNoTracking()
				.Where(x => x.UserId == userId)
				.Where(x => x.WorkspaceId == workspaceId)
				.FirstOrDefaultAsync();
			return res != null;
		}

		public async Task<bool> IsMemberAdmin(int workspaceId, string userId)
		{
			var isAdmin = await _context.WorkspaceMembers
				.AsNoTracking()
				.Where(x => x.UserId == userId)
				.Where(x => x.WorkspaceId == workspaceId)
				.Select(x => x.IsAdmin)
				.FirstOrDefaultAsync();
			return isAdmin;
		}
	}
}
