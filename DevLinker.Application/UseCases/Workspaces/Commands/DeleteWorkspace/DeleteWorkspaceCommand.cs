using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Commands.DeleteWorkspace
{
	public record DeleteWorkspaceCommand(int Id) : IRequest<Result>;
}
