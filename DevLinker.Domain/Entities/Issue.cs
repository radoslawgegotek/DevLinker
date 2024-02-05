using DevLinker.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DevLinker.Domain.Entities
{
	public class Issue : AuditableEntity
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public IssueState State { get; set; }

		[ForeignKey("WorkspaceId")]
		public int WorkspaceId { get; set; }
		public Workspace? Workspace { get; set; }
	}

	public enum IssueState
	{
		InProgress, 
		Done,
		InReview
	}
}
