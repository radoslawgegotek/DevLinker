using DevLinker.Domain.Entities;
using System.Text.Json.Serialization;

namespace DevLinker.Domain.Dto
{
	public class IssueDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public IssueState State { get; set; }
		public int WorkspaceId { get; set; }
	}
}
