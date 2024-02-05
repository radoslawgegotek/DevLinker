using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using MediatR;

namespace DevLinker.Application.UseCases.Issues.Queries.GetIssues
{
    public class GetIssuesQuery : PageProperties, IRequest<Result<Page<IssueDto>>>
    {
        public int WorkspaceId { get; set; }
    }
}
