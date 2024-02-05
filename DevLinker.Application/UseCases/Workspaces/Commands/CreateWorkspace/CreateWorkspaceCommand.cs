using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Commands.CreateWorkspace
{
	public record CreateWorkspaceCommand(string Title, string Description) : IRequest<Result>;
}
