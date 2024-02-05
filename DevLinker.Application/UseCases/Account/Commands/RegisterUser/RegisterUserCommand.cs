using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.Account.Commands.RegisterUser
{
	public record RegisterUserCommand(
		string Email,
		string FirstName,
		string LastName,
		string Password, 
		string ConfirmPassword) : IRequest<Result>;
}
