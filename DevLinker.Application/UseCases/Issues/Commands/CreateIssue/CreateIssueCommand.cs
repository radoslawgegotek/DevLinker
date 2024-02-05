using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace DevLinker.Application.UseCases.Issues.Commands.CreateIssue
{
	public record CreateIssueCommand : IRequest<Result>
	{
		public string Title { get; set; }
		public string Description { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public IssueState State { get; set; }
		public int WorkspaceId { get; set; }
	}
}
