namespace DevLinker.Domain.Common
{
	public class AuditableSoftDeleteEntity : AuditableEntity
	{
		public bool IsDeleted { get; set; }
		public DateTime? DeletedOn { get; set; }
		public string? DeletedBy { get; set; }
	}
}
