using Npgsql;

namespace DevLinker.Domain.IQueries
{
	public interface ISqlConnectionFactory
	{
		NpgsqlConnection CreateConnection();
	}
}
