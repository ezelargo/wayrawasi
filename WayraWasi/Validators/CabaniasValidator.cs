using FluentValidation;
using WayraWasi.Controllers;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Validators
{
    public class CabaniasValidator : AbstractValidator<Cabania> // Sirve para proporcionar una manera fácil de definir reglas de validación para un tipo específico
    {
        private readonly CabaniaRepository _repository;
        public CabaniasValidator(CabaniaRepository repository)
        {
            _repository = repository;

            RuleSet("Eliminar", () =>
            {
                RuleFor(c => c)
                .Must(CabañaReservada)
                .WithMessage("La cabaña se encuentra reservada. En caso de querer eliminarla primero elimine las reservas asignadas a la misma.");
            });

            RuleFor(c => c.NombreCabania)
            .NotEmpty().WithMessage("El nombre de la cabaña es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre de la cabaña no puede exceder los 50 caracteres.")
            .Must(NombreUnico).WithMessage("El nombre de la cabaña ya existe.");

            RuleFor(c => c.Descripcion).NotEmpty().WithMessage("La descripción es obligatoria.");

            RuleFor(c => c.Capacidad)
                .NotEmpty().WithMessage("La capacidad es obligatoria.")
                .GreaterThan(0).WithMessage("La capacidad debe ser mayor a cero.");

            RuleFor(c => c.PrecioNoche)
                .NotEmpty().WithMessage("El precio es obligatorio.")
                .GreaterThan(0).WithMessage("El precio por noche debe ser mayor a cero.");

            RuleFor(c => c.CheckIn)
                .NotEmpty().WithMessage("La hora de entrada es obligatoria.");

            RuleFor(c => c.CheckOut)
                .NotEmpty().WithMessage("La hora de salida es obligatoria.");

        }
        private bool NombreUnico(Cabania cabania, string nombreCabania) // Los tipos como cabania se toman automaticamente a la hora de comprobar los validadores
        {
            var NombreExistente = _repository.BuscarPorNombre(nombreCabania, cabania.IdCabania).Result;
            return NombreExistente == null;
        }

        private bool CabañaReservada(Cabania cabania)
        {
            var cabaniaReservada = _repository.BuscarReservaAsignadaACabania(cabania.IdCabania).Result;
            return !cabaniaReservada;
        }
    }
}
