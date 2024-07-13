using WayraWasi.Models;
using WayraWasi.ViewModels;

namespace WayraWasi.Data
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Cabania>> ListarDisponibilidadPorFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Reserva>> ProximasReservas(int dias);
    }
}
