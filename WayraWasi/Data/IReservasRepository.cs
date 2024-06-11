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
        Task<Cabania> BuscarCabaniaDisponibilidad(int id, DateTime? fechaInicio, DateTime? fechaFin);
        Task<int> Crear(Reserva model);
        Task<int> Editar(Reserva model);
        Task<int> Eliminar(int id);
    }
}
