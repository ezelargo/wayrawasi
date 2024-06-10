using Dapper;
using System.Data;
using WayraWasi.Models;
using WayraWasi.Data;

namespace WayraWasi.Data.Implementations
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DBDapperContext _conexionDapper;

        public HomeRepository(DBDapperContext conexionDapper)
        {
            _conexionDapper = conexionDapper;
        }

        public async Task<IEnumerable<Reserva>> ListarDisponibilidadPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Reserva>("" +
                    "SELECT Cabanias.NombreCabania, FechaEntrada, FechaSalida, Cabanias.Disponible FROM Reservaciones INNER JOIN Cabanias ON Cabanias.IdCabania = Reservaciones.IdCabania"
                    , new { FechaEntrada = fechaInicio, FechaSalida = fechaFin }); // Esta es la forma por la cual Dapper ingresa valores de busqueda para SQL
            }
        }
        /* public async Task<IEnumerable<Reserva>> GenerarReservaPorFecha(DateTime fechaInicio, DateTime fechaFin)
         {
             using (var conexionD = _conexionDapper.GetConnection())
             {
                 return await conexionD.QueryAsync<Reserva>("" +
                     "SELECT r.NombreCliente, r.FechaEntrada, r.FechaSalida, c.NombreCabania FROM Reservaciones r INNER JOIN Cabanias c ON c.IdCabania = r.IdCabania"
                     , new { FechaEntrada = fechaInicio, FechaSalida = fechaFin });
             }
         }*/
    }
}
