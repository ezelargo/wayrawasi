using WayraWasi.Models;
using static Dapper.SqlMapper;

namespace WayraWasi.Data
{
    public interface IReservasRepository
    {
        Task<Reserva> BuscadorId(int id);
        Task<Cabania> BuscarPorIDCabania(int id);
        Task<IEnumerable<Reserva>> ListarTodos();
        Task<IEnumerable<Cabania>> ListarCabanias();
        Task<bool> BuscarCabaniaDisponibilidad(Reserva modelo, DateTime? fechaInicio, DateTime? fechaFin);
        Task<IEnumerable<Reserva>> GenerarReservaPorFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<int> ActualizarReservas(Reserva reserva);
        Task<int> Crear(Reserva model);
        Task<int> Editar(Reserva model);
        Task<int> Eliminar(int id);
    }
}
