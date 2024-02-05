using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.Account.Queries.SignOut
{
	public record SignOutUserQuery() : IRequest<Result>;
}
