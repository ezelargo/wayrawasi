using FluentValidation;
using iText.Commons.Bouncycastle.Security;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Validators
{
    public class ReservasValidator : AbstractValidator<Reserva>
    {
        private readonly ReservaRepository _repository;
        public ReservasValidator(ReservaRepository repository)
        {

            _repository = repository;

            RuleFor(r => r.NombreCliente)
                .NotEmpty().WithMessage("El nombre del cliente es obligatorio.");

            RuleFor(r => r.FechaEntrada)
                .NotNull().WithMessage("La fecha de entrada es obligatoria.")
                .LessThan(r => r.FechaSalida).WithMessage("La fecha de entrada debe ser anterior a la fecha de salida.");

            RuleFor(r => r.FechaSalida)
                .NotNull().WithMessage("La fecha de salida es obligatoria.");

            RuleFor(r => r.NumeroPersonas)
                .NotEmpty().WithMessage("La cantida de personas es obligatorio.")
                .GreaterThan(0).WithMessage("El número de personas debe ser mayor a cero.");

            RuleFor(r => r.IdCabania).GreaterThan(0).WithMessage("Debe seleccionar una cabaña.");

            RuleFor(reserva => reserva)
            .Must(DisponibilidadCabaña).WithMessage("La cabaña ya se encuentra reservada en esas fechas.");

            RuleFor(reserva => reserva.NumeroPersonas)
                .Must(CapacidadMaxima)
                .WithMessage(reserva => $"El número de personas excede la capacidad de la cabaña seleccionada. La capacidad máxima es de {ObtenerCapacidadCabania(reserva.IdCabania)}.");
        }

        private bool DisponibilidadCabaña(Reserva reserva)
        {
            var cabaniaOcupada = _repository.BuscarCabaniaDisponibilidad(reserva, reserva.FechaEntrada, reserva.FechaSalida).Result;
            return !cabaniaOcupada;
        }

        private bool CapacidadMaxima(Reserva reserva, int numeroPersonas)
        {
            var cabania = _repository.BuscarPorIDCabania(reserva.IdCabania).Result;
            if(cabania == null)
            {
                return false;
            }
            return numeroPersonas <= cabania.Capacidad;
        }

        private long ObtenerCapacidadCabania(int idCabania)
        {
                Cabania cabaniaCapacidad = _repository.BuscarPorIDCabania(idCabania).Result;
                return cabaniaCapacidad.Capacidad;
        }
    }
}
