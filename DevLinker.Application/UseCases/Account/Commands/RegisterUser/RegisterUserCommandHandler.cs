using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DevLinker.Application.UseCases.Account.Commands.RegisterUser
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserStore<AppUser> _userStore;
		private readonly IValidator<RegisterUserCommand> _validator;
		private readonly IMapper _mapper;

		public RegisterUserCommandHandler(
			UserManager<AppUser> userManager,
			IUserStore<AppUser> userStore,
			IValidator<RegisterUserCommand> validator,
			IMapper mapper)
		{
			_userManager = userManager;
			_userStore = userStore;
			_validator = validator;
			_mapper = mapper;
		}


        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
				return Result.Fail(validationResult.ToDictionary());

            AppUser user = _mapper.Map<AppUser>(request);

			await _userStore.SetUserNameAsync(user, request.Email, cancellationToken);
			var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
				await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));
				await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.FirstName));
				await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));

				return Result.Ok();
			}
			return Result.Fail(result.Errors.ToDictionary(c => c.Code, des => new string[] { des.Description }));
		}
	}
}
