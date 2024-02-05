using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.WorkspacesMembers.Commands.AddWorkspaceMember
{
	public class AddWorkspaceMemberCommandHandler : IRequestHandler<AddWorkspaceMemberCommand, Result>
	{
		private readonly IWorkspaceMemberRepository _workspaceMemberRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IValidator<AddWorkspaceMemberCommand> _validator;

		public AddWorkspaceMemberCommandHandler(
			IWorkspaceMemberRepository workspaceMemberRepository,
			IUnitOfWork unitOfWork,
			IMapper mapper,
			IValidator<AddWorkspaceMemberCommand> validator)
		{
			_workspaceMemberRepository = workspaceMemberRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<Result> Handle(AddWorkspaceMemberCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
				var workspaceMember = _mapper.Map<WorkspaceMember>(request);
				await _workspaceMemberRepository.CreateAsync(workspaceMember);
				await _unitOfWork.SaveAsync();
				return Result.Ok();
			}
			return Result.Fail(validationResult.ToDictionary());
		}
	}
}
