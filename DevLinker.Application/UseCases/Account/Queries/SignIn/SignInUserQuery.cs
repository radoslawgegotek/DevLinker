
using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.Account.Queries.SignIn
{
	public record SignInUserQuery(string Username, string Password) : IRequest<Result>;
}
