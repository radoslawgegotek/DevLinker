using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Domain.IServices;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.Workspaces.Commands.CreateWorkspace
{
	public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, Result>
	{
		private readonly IWorkspaceRepository _workspaceRepository;
		private readonly IWorkspaceMemberRepository _memberRepository;
		private readonly ICurrentUserService _currentUserService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<CreateWorkspaceCommand> _validator;
		private readonly IMapper _mapper;

		public CreateWorkspaceCommandHandler(
			IWorkspaceRepository workspaceRepository,
			IUnitOfWork unitOfWork,
			IValidator<CreateWorkspaceCommand> validator,
			IMapper mapper,
			IWorkspaceMemberRepository memberRepository,
			ICurrentUserService currentUserService)
		{
			_workspaceRepository = workspaceRepository;
			_unitOfWork = unitOfWork;
			_validator = validator;
			_mapper = mapper;
			_memberRepository = memberRepository;
			_currentUserService = currentUserService;
		}

		public async Task<Result> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if(validationResult.IsValid)
			{
				var workspace = _mapper.Map<Workspace>(request);
				var workspaceMember = new WorkspaceMember()
				{
					UserId = _currentUserService.GetCurrnetUserId(),
					Workspace = workspace,
					IsAdmin = true
				};

				await _workspaceRepository.CreateAsync(workspace);
				await _memberRepository.CreateAsync(workspaceMember);
				await _unitOfWork.SaveAsync();
				return Result.Ok();
			}
			return Result.Fail(validationResult.ToDictionary());
		}
	}
}
