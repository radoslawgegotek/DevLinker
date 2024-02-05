using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using MediatR;

namespace DevLinker.Application.UseCases.Account.Queries.GetUserInfo
{
	public record GetUserInfoQuery() : IRequest<Result<UserInfoDto>>;
}
