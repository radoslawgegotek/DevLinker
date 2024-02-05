using Dapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.IQueries;
using Npgsql;

namespace DevLinker.Infrastructure.Queries
{
	public class IssueQuery : IIssueQuery
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public IssueQuery(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<Page<IssueDto>> GetIssuesAsync(PageProperties properties, int workspaceId)
		{
			await using NpgsqlConnection connection = _connectionFactory.CreateConnection();

			int offset = (properties.PageNumber - 1) * properties.PageSize;
			string sqlQuery = @"SELECT ""Issues"".""Id"", ""Issues"".""Title"", ""Issues"".""Description"", ""Issues"".""State"", ""Issues"".""WorkspaceId""
								FROM ""Issues""
								WHERE ""Issues"".""WorkspaceId"" = @WorkspaceId
								ORDER BY @OrderBy
								LIMIT @PageSize OFFSET @Offset";

			var result = await connection.QueryAsync<IssueDto>(sqlQuery, new
			{
				WorkspaceId = workspaceId,
				properties.OrderBy,
				properties.PageSize,
				Offset = offset
			});

			return new Page<IssueDto>() 
			{
				PageNumber = properties.PageNumber,
				PageSize = properties.PageSize,
				TotalCount = result.Count(),
				Items = result.ToList()
			};
		}
	}
}
