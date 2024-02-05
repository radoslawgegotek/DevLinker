using DevLinker.Application.UseCases.Workspaces.Commands.CreateWorkspace;
using DevLinker.Application.UseCases.Workspaces.Commands.DeleteWorkspace;
using DevLinker.Application.UseCases.Workspaces.Commands.UpdateWorkspace;
using DevLinker.Application.UseCases.Workspaces.Queries.GetUserWorkspaces;
using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevLinker.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class WorkspaceController : ControllerBase
	{
		private readonly IMediator _mediator;

		public WorkspaceController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<Result<List<UserWorkspacesDto>>>> GetUserWorkspaces([FromQuery] GetUserWorkspacesQuery query)
		{
			var result = await _mediator.Send(query);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		public async Task<ActionResult<Result>> CreateWorkspace([FromBody] CreateWorkspaceCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPut("{Id}")]
		public async Task<ActionResult<Result>> UpdateWorkspace([FromRoute] int Id, [FromBody] UpdateWorkspaceCommand command)
		{
			command.Id = Id;
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{Id}")]
		public async Task<ActionResult<Result>> DeleteWorkspace([FromRoute] DeleteWorkspaceCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
	}
}
