using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.Repositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Commands.UpdateWorkspace
{
	public class UpdateWorkspaceCommandHandler : IRequestHandler<UpdateWorkspaceCommand, Result>
	{
		private readonly IMapper _mapper;
		private readonly IWorkspaceRepository _workspaceRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<UpdateWorkspaceCommand> _validator;

		public UpdateWorkspaceCommandHandler(
			IMapper mapper,
			IWorkspaceRepository workspaceRepository,
			IUnitOfWork unitOfWork,
			IValidator<UpdateWorkspaceCommand> validator)
		{
			_mapper = mapper;
			_workspaceRepository = workspaceRepository;
			_unitOfWork = unitOfWork;
			_validator = validator;
		}

		public async Task<Result> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if (validationResult.IsValid)
			{
				try
				{
					var workspace = await _workspaceRepository.GetByIdAsync(request.Id);

					_mapper.Map(request, workspace);

					_workspaceRepository.Update(workspace);
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
