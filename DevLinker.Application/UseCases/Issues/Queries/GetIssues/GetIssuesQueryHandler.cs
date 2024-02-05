using DevLinker.Domain.Common;
using DevLinker.Domain.Dto;
using DevLinker.Domain.IQueries;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.Issues.Queries.GetIssues
{
    public class GetIssuesQueryHandler : IRequestHandler<GetIssuesQuery, Result<Page<IssueDto>>>
    {
        private readonly IIssueQuery _query;
        private readonly IValidator<GetIssuesQuery> _validator;

        public GetIssuesQueryHandler(
            IIssueQuery query,
            IValidator<GetIssuesQuery> validator)
        {
            _query = query;
            _validator = validator;
        }

        public async Task<Result<Page<IssueDto>>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                var dto = await _query.GetIssuesAsync(request, request.WorkspaceId);
                return Result.Ok(dto);
            }
            return Result.Fail<Page<IssueDto>>(validationResult.ToDictionary());
        }
    }
}
