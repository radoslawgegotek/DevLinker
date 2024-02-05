using DevLinker.Domain.Common;
using MediatR;

namespace DevLinker.Application.UseCases.Issues.Commands.DeleteIssue
{
	public record DeleteIssueCommand(int Id) : IRequest<Result>;
}
