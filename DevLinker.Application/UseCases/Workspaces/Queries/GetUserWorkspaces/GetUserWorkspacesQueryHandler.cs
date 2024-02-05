using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.IQueries;
using DevLinker.Domain.IServices;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Queries.GetUserWorkspaces
{
	public class GetUserWorkspacesQueryHandler : IRequestHandler<GetUserWorkspacesQuery, Result<List<UserWorkspacesDto>>>
	{
		private readonly IWorkspaceQuery _query;
		private readonly ICurrentUserService _currentUserService;

		public GetUserWorkspacesQueryHandler(
			IWorkspaceQuery query, 
			ICurrentUserService currentUserService)
		{
			_query = query;
			_currentUserService = currentUserService;
		}

		public async Task<Result<List<UserWorkspacesDto>>> Handle(GetUserWorkspacesQuery request, CancellationToken cancellationToken)
		{
			var dto = await _query.GetUserWorkspaces(_currentUserService.GetCurrnetUserId());
			return Result.Ok(dto);
		}
	}
}
