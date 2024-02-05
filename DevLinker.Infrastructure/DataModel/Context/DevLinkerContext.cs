using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevLinker.Infrastructure.DataModel.Context
{
	public class DevLinkerContext : IdentityDbContext<AppUser>
	{
		private readonly ICurrentUserService _currentUserService;

		public DevLinkerContext(DbContextOptions<DevLinkerContext> options, ICurrentUserService currentUserService) : base(options)
		{
			_currentUserService = currentUserService;
		}

		public DbSet<Issue> Issues { get; set; }
		public DbSet<Workspace> Workspaces { get; set; }
		public DbSet<WorkspaceMember> WorkspaceMembers { get; set; }
		public DbSet<IssueMember> IssueMembers { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.GetCurrnetUserId();
						entry.Entity.CreatedOn = DateTime.Now;
						break;

					case EntityState.Modified:
						entry.Entity.UpdatedBy = _currentUserService.GetCurrnetUserId();
						entry.Entity.UpdatedOn = DateTime.Now;
						break;
				}
			}

            foreach (var entry in ChangeTracker.Entries<AuditableSoftDeleteEntity>())
            {
                if (entry.State == EntityState.Deleted)
                {
					entry.State = EntityState.Modified;
					entry.Entity.DeletedOn = DateTime.Now;
					entry.Entity.DeletedBy = _currentUserService.GetCurrnetUserId();
					entry.Entity.IsDeleted = true;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
		}
	}
}
