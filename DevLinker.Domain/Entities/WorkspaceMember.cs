using DevLinker.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevLinker.Domain.Entities
{
	public class WorkspaceMember : AuditableEntity
	{
		public int Id { get; set; }

		[ForeignKey("UserId")]
		public string UserId { get; set; }
		public AppUser? User { get; set; }

		[ForeignKey("WorkspaceId")]
		public int WorkspaceId { get; set; }
		public Workspace? Workspace { get; set; }

		public bool IsAdmin { get; set; }
	}
}
