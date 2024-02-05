using System.Security.Claims;

namespace DevLinker.Domain.Dto
{
	public class UserInfoDto
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public Dictionary<string,string> Claims { get; set; } = new Dictionary<string,string>();
	}
}
