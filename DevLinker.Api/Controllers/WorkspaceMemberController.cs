using DevLinker.Application.UseCases.WorkspacesMembers.Commands.AddWorkspaceMember;
using DevLinker.Application.UseCases.WorkspacesMembers.Commands.DeleteWorkspaceMember;
using DevLinker.Application.UseCases.WorkspacesMembers.Queries.GetWorkspaceMembers;
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
	public class WorkspaceMemberController : ControllerBase
	{
		private readonly IMediator _mediator;

		public WorkspaceMemberController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<Result<List<UserWorkspacesDto>>>> GetWorkspaceMembers([FromQuery] GetWorkspaceMembersQuery query)
		{
			var result = await _mediator.Send(query);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		public async Task<ActionResult<Result>> AddWorkspaceMember([FromBody] AddWorkspaceMemberCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete]
		public async Task<ActionResult<Result>> DeleteWorkspaceMember([FromQuery] DeleteWorkspaceMemberCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
	}
}
