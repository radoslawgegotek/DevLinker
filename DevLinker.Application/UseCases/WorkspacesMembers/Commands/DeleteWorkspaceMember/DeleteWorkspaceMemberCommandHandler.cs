using DevLinker.Domain.Common;
using DevLinker.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Commands.DeleteWorkspaceMember
{
	public class DeleteWorkspaceMemberCommandHandler : IRequestHandler<DeleteWorkspaceMemberCommand, Result>
	{
		private readonly IWorkspaceMemberRepository _workspaceMemberRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<DeleteWorkspaceMemberCommand> _validator;

		public DeleteWorkspaceMemberCommandHandler(
			IWorkspaceMemberRepository workspaceMemberRepository, 
			IUnitOfWork unitOfWork, 
			IValidator<DeleteWorkspaceMemberCommand> validator)
		{
			_workspaceMemberRepository = workspaceMemberRepository;
			_unitOfWork = unitOfWork;
			_validator = validator;
		}

		public async Task<Result> Handle(DeleteWorkspaceMemberCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);

			if (validationResult.IsValid) 
			{
				try
				{
					var workspaceMember = await _workspaceMemberRepository.GetByIdAsync(request.WorkspaceMemberId);
					
					_workspaceMemberRepository.Delete(workspaceMember);
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
