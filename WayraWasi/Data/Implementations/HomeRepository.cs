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
                var parametros = new DynamicParameters();
                parametros.Add("@FechaEntrada", fechaInicio, DbType.Int32);
                parametros.Add("@FechaSalida", fechaFin, DbType.Int32);
                return await conexionD.QueryAsync<Reserva>("ListarDisponibilidadPorFecha", parametros, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<Reserva>> GenerarReservaPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@FechaEntrada", fechaInicio, DbType.Int32);
                parametros.Add("@FechaSalida", fechaFin, DbType.Int32);
                return await conexionD.QueryAsync<Reserva>("GenerarReservaPorFecha", parametros, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
