using DevLinker.Application.UseCases.Account.Commands.RegisterUser;
using DevLinker.Application.UseCases.Account.Queries.GetUserInfo;
using DevLinker.Application.UseCases.Account.Queries.SignIn;
using DevLinker.Application.UseCases.Account.Queries.SignOut;
using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevLinker.Api.Controllers
{
	[Route("auth/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AccountController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Authorize]
		[HttpGet("userinfo")]
		public async Task<ActionResult<Result<UserInfoDto>>> GetUserInfo([FromQuery] GetUserInfoQuery query)
		{
			var result = await _mediator.Send(query);

			return result.IsSuccess ? Ok(result) : Unauthorized(result);
		}

		[HttpPost("register")]
		public async Task<ActionResult<Result>> Register([FromBody]RegisterUserCommand command)
		{
			var result = await _mediator.Send(command);

			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost("signin")]
		public async Task<ActionResult<Result>> SignIn([FromBody]SignInUserQuery query)
		{
			var result = await _mediator.Send(query);

			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost("signout")]
		public async Task<ActionResult<Result>> SignOut([FromQuery]SignOutUserQuery query)
		{
			var result = await _mediator.Send(query);

			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
	}
}
