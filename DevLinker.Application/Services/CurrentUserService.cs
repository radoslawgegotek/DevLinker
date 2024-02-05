using DevLinker.Domain.IServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DevLinker.Application.Services
{
	public class CurrnetUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _contextAccessor;
		public CurrnetUserService(IHttpContextAccessor httpContextAccessor)
		{
			_contextAccessor = httpContextAccessor;
		}

		public string GetCurrnetUserId()
		{
			var claimsPrincipal = _contextAccessor?.HttpContext?.User as ClaimsPrincipal;
			return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
