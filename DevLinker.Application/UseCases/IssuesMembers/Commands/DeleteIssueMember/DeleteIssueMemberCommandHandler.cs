using DevLinker.Domain.Common;
using DevLinker.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.IssuesMembers.Commands.DeleteIssueMember
{
	public class DeleteIssueMemberCommandHandler : IRequestHandler<DeleteIssueMemberCommand, Result>
	{
		private readonly IWorkspaceMemberRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<DeleteIssueMemberCommand> _validator;

		public DeleteIssueMemberCommandHandler(
			IWorkspaceMemberRepository repository, 
			IUnitOfWork unitOfWork, 
			IValidator<DeleteIssueMemberCommand> validator)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
			_validator = validator;
		}

		public async Task<Result> Handle(DeleteIssueMemberCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);

			if (validationResult.IsValid)
			{
				try
				{
					var issueMember = await _repository.GetByIdAsync(request.IssueMemberId);

					_repository.Delete(issueMember);
					await _unitOfWork.SaveAsync();
					return Result.Ok();
				}
				catch (NullReferenceException)
				{
					return Result.Fail();
				}
			}
			return Result.Fail(validationResult.ToDictionary());
		}
	}
}
