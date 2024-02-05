using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace DevLinker.Application.UseCases.Issues.Commands.UpdateIssue
{
	public class UpdateIssueCommand : IRequest<Result>
	{
		public int Id { get; set; }
		public int WorkspaceId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public IssueState State { get; set; }
	}
}