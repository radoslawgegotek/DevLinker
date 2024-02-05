using DevLinker.Domain.Common;
using DevLinker.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Commands.DeleteWorkspace
{
	public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, Result>
	{
		private readonly IWorkspaceRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<DeleteWorkspaceCommand> _validator;

		public DeleteWorkspaceCommandHandler(
			IWorkspaceRepository repository, 
			IUnitOfWork unitOfWork, 
			IValidator<DeleteWorkspaceCommand> validator)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
			_validator = validator;
		}

		public async Task<Result> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);
            if (validationResult.IsValid)
            {
				try
				{
					var workspace = await _repository.GetByIdAsync(request.Id);

					_repository.Delete(workspace);
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
