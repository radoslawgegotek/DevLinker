using System;

namespace DevLinker.Domain.IRepositories
{
	public interface IBaseRepository<T> where T : class
	{
		Task CreateAsync(T entity);
		void Update(T entity);
		Task<T> GetByIdAsync(int id);
		void Delete(T entity);
	}
}
