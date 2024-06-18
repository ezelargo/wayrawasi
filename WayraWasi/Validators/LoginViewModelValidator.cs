using FluentValidation;
using WayraWasi.ViewModels;

namespace WayraWasi.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator() 
        {
            RuleFor(l => l.Email).NotEmpty().EmailAddress();
            RuleFor(l => l.Password).NotEmpty().MinimumLength(8);
        }
    }
}
