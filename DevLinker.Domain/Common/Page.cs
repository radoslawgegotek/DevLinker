namespace DevLinker.Domain.Common
{
	public class Page<T> where T : class
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public IEnumerable<T> Items { get; set; }

		public Page()
		{
			Items = new List<T>();
			TotalCount = 0;
		}
	}
}
