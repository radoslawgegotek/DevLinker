using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.IssuesMembers.Commands.AddIssueMember
{
	public record AddIssueMemberCommand(int WorkspaceId, int IssueId, string UserId) : IRequest<Result>;
}
