using DevLinker.Domain.IQueries;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DevLinker.Infrastructure.DataModel
{
	public class SqlConnectionFactory : ISqlConnectionFactory
	{
		private readonly IConfiguration _configuration;

		public SqlConnectionFactory(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public NpgsqlConnection CreateConnection()
		{
			return new NpgsqlConnection(
				_configuration.GetConnectionString("DefaultPgConnection"));
		}
	}
}
