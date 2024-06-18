using FluentValidation;
using WayraWasi.ViewModels;

namespace WayraWasi.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator() 
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(8);
            RuleFor(r => r.ConfirmPassword).
                Equal(r => r.Password).WithMessage("No es la misma contraseña.");
        }
    }
}
