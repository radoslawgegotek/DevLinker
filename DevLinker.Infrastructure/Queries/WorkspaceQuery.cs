using DevLinker.Domain.IQueries;
using Dapper;
using Npgsql;
using DevLinker.Domain.Dto;

namespace DevLinker.Infrastructure.Queries
{
	public class WorkspaceQuery : IWorkspaceQuery
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public WorkspaceQuery(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<List<UserWorkspacesDto>> GetUserWorkspaces(string userId)
		{
			await using NpgsqlConnection connection = _connectionFactory.CreateConnection();

			var sqlResponse = await connection.QueryAsync<UserWorkspacesDto>(
				@"SELECT ""Id"", ""Title"", ""Description"", ""CreatedBy"", ""CreatedOn"", ""UpdatedBy"", ""UpdatedOn""
				  FROM ""Workspaces""
				  WHERE ""CreatedBy"" = @UserId;", new { UserId = userId });

			return sqlResponse.ToList();
		}
	}
}
