using DevLinker.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevLinker.Domain.Entities
{
	public class IssueMember : AuditableEntity
	{
		public int Id { get; set; }

		[ForeignKey("UserId")]
		public string UserId { get; set; }
		public AppUser? User { get; set; }

		[ForeignKey("IssueId")]
		public int IssueId { get; set; }
		public Issue? Issue { get; set; }
	}
}
