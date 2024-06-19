using FluentValidation;
using WayraWasi.Controllers;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Validators
{
    public class CabaniasValidator : AbstractValidator<Cabania>
    {
        private readonly CabaniaRepository _repository;
        public CabaniasValidator(CabaniaRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.NombreCabania)
            .NotEmpty().WithMessage("El nombre de la cabaña es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre de la cabaña no puede exceder los 50 caracteres.")
            .Must(NombreUnico).WithMessage("El nombre de la cabaña ya existe.");
                
            RuleFor(c => c.Descripcion).NotEmpty().WithMessage("La descripción es obligatoria.");
            RuleFor(c => c.Capacidad).GreaterThan(0).WithMessage("La capacidad debe ser mayor a cero.");
            RuleFor(c => c.PrecioNoche).GreaterThan(0).WithMessage("El precio por noche debe ser mayor a cero.");

            RuleFor(c => c)
            .Must(CabañaReservada).WithMessage("La cabaña se encuentra reservada. Si la elimina tambien se eliminaran todas las reservas asignadas a la cabaña.");

        }
        private  bool NombreUnico(string nombreCabania)
        {
            var NombreExistente = _repository.BuscarPorNombre(nombreCabania).Result;
            return NombreExistente == null;
        }

        private bool CabañaReservada(Cabania cabania)
        {
            var cabaniaReservada = _repository.BuscarReservaAsignadaACabania(cabania.IdCabania).Result;
            return cabaniaReservada == null;
        }
    }
}
