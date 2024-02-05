using Dapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.IQueries;
using Npgsql;

namespace DevLinker.Infrastructure.Queries
{
	public class WorkspaceMemberQuery : IWorkspaceMemberQuery
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public WorkspaceMemberQuery(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<Page<MemberDto>> GetWorkspaceMembers(PageProperties properties, int workspaceId)
		{
			await using NpgsqlConnection connection = _connectionFactory.CreateConnection();

			int offset = (properties.PageNumber - 1) * properties.PageSize;
			string sqlQuery = @"SELECT ""WorkspaceMembers"".""Id"", ""WorkspaceMembers"".""UserId"", ""AspNetUsers"".""FirstName"", ""AspNetUsers"".""LastName"", ""AspNetUsers"".""Email""
								FROM ""AspNetUsers""
								INNER JOIN ""WorkspaceMembers"" ON ""WorkspaceMembers"".""UserId"" = ""AspNetUsers"".""Id""
								WHERE ""WorkspaceMembers"".""WorkspaceId"" = @WorkspaceId
								ORDER BY @OrderBy
								LIMIT @PageSize OFFSET @Offset";
			
			var result = await connection.QueryAsync<MemberDto>(sqlQuery, 
				new
				{
					WorkspaceId = workspaceId,
					properties.OrderBy,
					properties.PageSize,
					Offset = offset
				});

			return new Page<MemberDto>
			{
				PageNumber = properties.PageNumber,
				PageSize = properties.PageSize,
				TotalCount = result.Count(),
				Items = result.ToList()
			};
		}
	}
}
