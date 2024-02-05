using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.DataModel.Context;

namespace DevLinker.Infrastructure.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly DevLinkerContext _context;

		public BaseRepository(DevLinkerContext context)
		{
			_context = context;
		}

		public async Task CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id) ?? 
				throw new NullReferenceException($"Failed to find {nameof(T)} with id {id}");
		}
	}
}
