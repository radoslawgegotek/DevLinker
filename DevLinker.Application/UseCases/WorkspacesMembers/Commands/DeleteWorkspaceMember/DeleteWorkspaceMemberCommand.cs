using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Commands.DeleteWorkspaceMember
{
	public record DeleteWorkspaceMemberCommand(int WorkspaceMemberId) : IRequest<Result>;
}
