using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.IssuesMembers.Commands.DeleteIssueMember
{
	public record DeleteIssueMemberCommand(int IssueMemberId, int WorkspaceId) : IRequest<Result>;
}
