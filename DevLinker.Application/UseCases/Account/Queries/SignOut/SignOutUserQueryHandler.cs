using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLinker.Application.UseCases.Account.Queries.SignOut
{
	public class SignOutUserQueryHandler : IRequestHandler<SignOutUserQuery, Result>
	{
		private readonly SignInManager<AppUser> _signInManager;

		public SignOutUserQueryHandler(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public async Task<Result> Handle(SignOutUserQuery request, CancellationToken cancellationToken)
		{
			await _signInManager.SignOutAsync();
			return Result.Ok();
		}
	}
}
