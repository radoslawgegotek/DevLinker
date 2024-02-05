
namespace DevLinker.Domain.IRepositories
{
	public interface IUnitOfWork
	{
		Task SaveAsync();
	}
}
