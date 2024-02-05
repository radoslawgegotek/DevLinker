using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Commands.UpdateWorkspace
{
	public class UpdateWorkspaceCommand : IRequest<Result>
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
