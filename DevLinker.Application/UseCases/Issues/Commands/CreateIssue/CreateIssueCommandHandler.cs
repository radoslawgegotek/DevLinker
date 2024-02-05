using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.Issues.Commands.CreateIssue
{
	public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, Result>
	{
		private readonly IIssueRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<CreateIssueCommand> _validator;
		private readonly IMapper _mapper;

		public CreateIssueCommandHandler(
			IIssueRepository repository,
			IValidator<CreateIssueCommand> validator,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_validator = validator;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);
			if (validationResult.IsValid)
			{
				var issue = _mapper.Map<Issue>(request);
				await _repository.CreateAsync(issue);
				await _unitOfWork.SaveAsync();
				return Result.Ok();
			}
			return Result.Fail(validationResult.ToDictionary());
		}
	}
}
