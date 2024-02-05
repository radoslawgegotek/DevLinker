namespace DevLinker.Domain.Common
{
	public class PageProperties
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; } = 10;
		public string OrderBy { get; set; }
    }
}
