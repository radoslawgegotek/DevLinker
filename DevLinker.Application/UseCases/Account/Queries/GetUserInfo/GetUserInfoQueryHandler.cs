using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DevLinker.Application.UseCases.Account.Queries.GetUserInfo
{
	public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, Result<UserInfoDto>>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;

		public GetUserInfoQueryHandler(
			UserManager<AppUser> userManager,
			ICurrentUserService currentUserService,
			IMapper mapper)
		{
			_userManager = userManager;
			_currentUserService = currentUserService;
			_mapper = mapper;
		}

		public async Task<Result<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(_currentUserService.GetCurrnetUserId());

			if (user != null)
			{
				var userInfo = _mapper.Map<UserInfoDto>(user);

				var claims = await _userManager.GetClaimsAsync(user);
                
				foreach(var claim in claims)
				{
					userInfo.Claims.Add(claim.Type, claim.Value);
				}

                return Result.Ok(userInfo);
			}

			return Result.Fail<UserInfoDto>();
		}
	}
}
