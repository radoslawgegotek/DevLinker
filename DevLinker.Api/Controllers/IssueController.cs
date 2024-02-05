using DevLinker.Application.UseCases.Issues.Commands.CreateIssue;
using DevLinker.Application.UseCases.Issues.Commands.DeleteIssue;
using DevLinker.Application.UseCases.Issues.Commands.UpdateIssue;
using DevLinker.Application.UseCases.Issues.Queries.GetIssues;
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
	public class IssueController : ControllerBase
	{
		private readonly IMediator _mediator;

		public IssueController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<Result<Page<IssueDto>>>> GetIssues([FromQuery] GetIssuesQuery query)
		{
			var result = await _mediator.Send(query);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		public async Task<ActionResult<Result>> CreateIssue(CreateIssueCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPut("{Id}")]
		public async Task<ActionResult<Result>> UpdateIssue([FromRoute] int Id, [FromBody] UpdateIssueCommand command)
		{
			command.Id = Id;
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{Id}")]
		public async Task<ActionResult<Result>> DeleteIssues([FromRoute] DeleteIssueCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
	}
}
