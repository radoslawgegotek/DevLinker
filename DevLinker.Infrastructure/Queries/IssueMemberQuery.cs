using Dapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IQueries;
using Npgsql;

namespace DevLinker.Infrastructure.Queries
{
	public class IssueMemberQuery : IIssueMemberQuery
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public IssueMemberQuery(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<Page<MemberDto>> GetIssueMembers(PageProperties properties, int issueId)
		{
			await using NpgsqlConnection connection = _connectionFactory.CreateConnection();

			int offset = (properties.PageNumber - 1) * properties.PageSize;

			string sqlQuery = @"SELECT ""IssueMembers"".""Id"", ""IssueMembers"".""UserId"", ""AspNetUsers"".""FirstName"", ""AspNetUsers"".""LastName"", ""AspNetUsers"".""Email""
								FROM ""AspNetUsers""
								INNER JOIN ""IssueMembers"" ON ""IssueMembers"".""UserId"" = ""AspNetUsers"".""Id""
								WHERE ""IssueMembers"".""IssueId"" = @IssueId
								ORDER BY @OrderBy
								LIMIT @PageSize OFFSET @Offset";

			var result = await connection.QueryAsync<MemberDto>(sqlQuery,
				new
				{
					IssueId = issueId,
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
