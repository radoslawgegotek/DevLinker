namespace DevLinker.Domain.Dto
{
	public record MemberDto(
		int Id,
		string UserId,
		string FirstName,
		string LastName,
		string Email);
}
