using FluentValidation;
using WayraWasi.ViewModels;

namespace WayraWasi.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage("El correo electronico es obligatorio")
                .EmailAddress().WithMessage("El formato del correo no es correcto");
            RuleFor(r => r.Password).NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("La contraseña es obligatoria");
            RuleFor(r => r.ConfirmPassword).
                Equal(r => r.Password).WithMessage("No es la misma contraseña.").
                NotEmpty().WithMessage("La confirmacion de la contraseña es obligatoria");
        }
    }
}
