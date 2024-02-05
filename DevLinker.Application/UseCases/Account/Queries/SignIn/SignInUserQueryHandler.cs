using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DevLinker.Application.UseCases.Account.Queries.SignIn
{
	public class SignInUserQueryHandler : IRequestHandler<SignInUserQuery, Result>
	{
		private readonly SignInManager<AppUser> _signInManager;
		

		public SignInUserQueryHandler(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public async Task<Result> Handle(SignInUserQuery request, CancellationToken cancellationToken)
		{
			var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
			if (result.Succeeded) 
			{
				return Result.Ok();
			}
			return Result.Fail();
		}
	}
}
