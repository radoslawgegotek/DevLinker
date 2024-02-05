using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.IssuesMembers.Commands.AddIssueMember
{
	public class AddIssueMemberCommandHandler : IRequestHandler<AddIssueMemberCommand, Result>
	{
		private readonly IIssueMemberRepository _issueMemberRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IValidator<AddIssueMemberCommand> _validator;

		public AddIssueMemberCommandHandler(
			IIssueMemberRepository issueRepository,
			IUnitOfWork unitOfWork,
			IMapper mapper,
			IValidator<AddIssueMemberCommand> validator)
		{
			_issueMemberRepository = issueRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<Result> Handle(AddIssueMemberCommand request, CancellationToken cancellationToken)
		{
			var validationResult =  await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
				var issueMember = _mapper.Map<IssueMember>(request);
				await _issueMemberRepository.CreateAsync(issueMember);
				await _unitOfWork.SaveAsync();
				return Result.Ok();
            }
			return Result.Fail(validationResult.ToDictionary());
        }
	}
}
