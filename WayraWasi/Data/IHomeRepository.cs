using WayraWasi.Models;

namespace WayraWasi.Data
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Cabania>> ListarDisponibilidadPorFecha(DateTime fechaInicio, DateTime fechaFin);

        //Task<IEnumerable<Reserva>> GenerarReservaPorFecha(DateTime fechaInicio, DateTime fechaFin);
    }
}
