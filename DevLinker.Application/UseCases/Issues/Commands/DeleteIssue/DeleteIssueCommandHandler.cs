using DevLinker.Domain.Common;
using DevLinker.Domain.IRepositories;
using MediatR;

namespace DevLinker.Application.UseCases.Issues.Commands.DeleteIssue
{
	public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, Result>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IIssueMemberRepository _issueMemberRepository;

		public DeleteIssueCommandHandler(IUnitOfWork unitOfWork, IIssueMemberRepository issueMemberRepository)
		{
			_unitOfWork = unitOfWork;
			_issueMemberRepository = issueMemberRepository;
		}

		public async Task<Result> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var issue = await _issueMemberRepository.GetByIdAsync(request.Id);

				_issueMemberRepository.Delete(issue);
				await _unitOfWork.SaveAsync();
				return Result.Ok();
			}
			catch (NullReferenceException)
			{
				return Result.Fail();
			}
		}
	}
}
