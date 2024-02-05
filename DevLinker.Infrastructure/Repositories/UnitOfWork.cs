using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.DataModel.Context;

namespace DevLinker.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DevLinkerContext _context;

		public UnitOfWork(DevLinkerContext context)
		{
			_context = context;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
