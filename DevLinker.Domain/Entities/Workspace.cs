using DevLinker.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevLinker.Domain.Entities
{
	public class Workspace : AuditableSoftDeleteEntity
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public List<WorkspaceMember> Members { get; set; } = new();
	}
}
