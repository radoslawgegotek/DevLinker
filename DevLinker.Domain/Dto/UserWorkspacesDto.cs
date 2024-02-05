namespace DevLinker.Domain.Dto
{
	public record class UserWorkspacesDto(
		int Id,
		string Title,
		string Description,
		string CreatedBy,
		DateTime CreatedOn,
		string UpdatedBy,
		DateTime UpdatedOn);
}
