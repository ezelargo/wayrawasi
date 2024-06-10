using WayraWasi.Models;
using static Dapper.SqlMapper;

namespace WayraWasi.Data
{
    public interface IReservasRepository
    {
        Task<Reserva> BuscadorId(int id);
        Task<Reserva> BuscarPorIDCabania(int id);
        Task<IEnumerable<Reserva>> ListarTodos();
        Task<int> Crear(Reserva model);
        Task<int> Editar(Reserva model);
        Task<int> Eliminar(int id);
    }
}
