using FluentValidation;

namespace DevLinker.Application.UseCases.Account.Commands.RegisterUser
{
	public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
	{
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull();
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
        }
    }
}
