using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Commands.AddWorkspaceMember
{
	public record AddWorkspaceMemberCommand(string UserId, int WorkspaceId, bool IsAdmin) : IRequest<Result>;
}
