using FluentValidation;
using WayraWasi.ViewModels;

namespace WayraWasi.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(l => l.Email).NotEmpty().WithMessage("El correo electronico es obligatorio")
                .EmailAddress().WithMessage("El formato del correo no es correcto");
            RuleFor(l => l.Password).NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres");
        }
    }
}
