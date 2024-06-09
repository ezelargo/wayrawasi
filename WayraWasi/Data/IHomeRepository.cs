using WayraWasi.Models;

namespace WayraWasi.Data
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Reserva>> ListarDisponibilidadPorFecha(DateTime fechaInicio, DateTime fechaFin);

        Task<IEnumerable<Reserva>> GenerarReservaPorFecha(DateTime fechaInicio, DateTime fechaFin);
    }
}
