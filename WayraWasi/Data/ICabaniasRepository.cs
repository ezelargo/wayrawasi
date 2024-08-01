using WayraWasi.Models;
using static Dapper.SqlMapper;

namespace WayraWasi.Data
{
    public interface ICabaniasRepository
    {
        Task<Cabania> BuscadorId(int id);
        Task<bool> BuscarReservaAsignadaACabania(int id);
        Task<IEnumerable<Cabania>> ListarTodos();
        Task<int> Crear(Cabania model);
        Task<int> Editar(Cabania model);
        Task<int> Eliminar(int id);
    }
}
