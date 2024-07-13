using Dapper;
using System.Data;
using WayraWasi.Models;
using WayraWasi.Data;
using WayraWasi.ViewModels;

namespace WayraWasi.Data.Implementations
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DBDapperContext _conexionDapper;

        public HomeRepository(DBDapperContext conexionDapper)
        {
            _conexionDapper = conexionDapper;
        }

        // Integran las dos fechas
        public async Task<IEnumerable<Cabania>> ListarDisponibilidadPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                // El CASE lo utilizo como un if donde si es NULL esta desocupada(Porque la cabaña no se asigna a ninguna reserva) y si no es NULL entonces esta ocupada
                return await conexionD.QueryAsync<Cabania>("ConsultarDisponibilidadCabanias"
                    , new { FechaEntrada = fechaInicio, FechaSalida = fechaFin },
                    commandType: CommandType.StoredProcedure); // Esta es la forma por la cual Dapper ingresa valores de busqueda para SQL
            }
        }

        public async Task<IEnumerable<Reserva>> ProximasReservas(int dias)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                // Este es un mapeo que se utiliza para controlar multiples campos en estos casos de ver distintos datos se debe utilizar
                return await conexionD.QueryAsync<Reserva, Cabania, Reserva>("ProximasReservas",
                    (reserva, cabania) => { reserva.Cabania = cabania; return reserva; }                    
                    , new { Dias = dias }
                    , splitOn: "IdCabania" // El spliton indica el campo por donde se van a dividir los resultados
                    , commandType: CommandType.StoredProcedure);
            }
        }

    }
}
