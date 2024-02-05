using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Queries.GetUserWorkspaces
{
	public record GetUserWorkspacesQuery() : IRequest<Result<List<UserWorkspacesDto>>>;
}
