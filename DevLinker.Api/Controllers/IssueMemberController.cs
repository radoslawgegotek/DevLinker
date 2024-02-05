using DevLinker.Application.UseCases.IssuesMembers.Commands.AddIssueMember;
using DevLinker.Application.UseCases.IssuesMembers.Commands.DeleteIssueMember;
using DevLinker.Application.UseCases.IssuesMembers.Queries.GetIssuesMembers;
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
	public class IssueMemberController : ControllerBase
	{
		private readonly IMediator _mediator;

		public IssueMemberController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<Result<Page<MemberDto>>>> GetIssueMembers([FromQuery] GetIssueMembersQuery query)
		{
			var result = await _mediator.Send(query);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		public async Task<ActionResult<Result>> AddIssueMember([FromBody] AddIssueMemberCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete]
		public async Task<ActionResult<Result>> DeleteIssueMember([FromQuery] DeleteIssueMemberCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
	}
}
