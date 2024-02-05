using AutoMapper;
using DevLinker.Domain.Common;
using DevLinker.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace DevLinker.Application.UseCases.Issues.Commands.UpdateIssue
{
	public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, Result>
	{
		private readonly IIssueRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IValidator<UpdateIssueCommand> _validator;
		private readonly IMapper _mapper;

		public UpdateIssueCommandHandler(
			IIssueRepository repository, 
			IUnitOfWork unitOfWork, 
			IValidator<UpdateIssueCommand> validator, 
			IMapper mapper)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
			_validator = validator;
			_mapper = mapper;
		}

		public async Task<Result> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);
			if (validationResult.IsValid)
			{
				var issue = await _repository.GetByIdAsync(request.Id);
				_mapper.Map(request, issue);
				_repository.Update(issue);
				await _unitOfWork.SaveAsync();
				return Result.Ok();
			}
			return Result.Fail(validationResult.ToDictionary());
		}
	}
}
