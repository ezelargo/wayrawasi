using Dapper;
using System.Data;
using WayraWasi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WayraWasi.Data.Implementations
{
    public class ReservaRepository : IReservasRepository
    {
        private readonly DBDapperContext _conexionDapper;

        public ReservaRepository(DBDapperContext conexionDapper)
        {
            _conexionDapper = conexionDapper;
        }

        public async Task<Reserva> BuscadorId(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Reserva>("sp_BuscarReservaId", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Cabania> BuscarPorIDCabania(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Cabania>("sp_BuscarCabaniaPorID", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Reserva>> ListarTodos()
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Reserva, Cabania, Reserva>("sp_ListarReservas", 
                                                                            (reserva, cabania) => { reserva.Cabania = cabania; return reserva; },
                                                                            splitOn: "IdCabania",
                                                                            commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Cabania>> ListarCabanias()
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Cabania>("sp_ListarCabanias", commandType: CommandType.StoredProcedure);
            }
        }
        
        public async Task<bool> BuscarCabaniaDisponibilidad(Reserva reserva,DateTime? fechaInicio, DateTime? fechaFin)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                // Esta consulta compara si la fecha de entrada coincide en el rango de fechas entre la entrada o la salida de alguna reserva y lo mismo para la Fecha de Salida. Teniendo asi que ni la entrada ni la salida se solapan con otra reserva.
                return await conexionD.QueryFirstOrDefaultAsync<bool>("sp_BuscarDisponibilidadCabania", 
                                                                        new { FechaEntrada = fechaInicio, FechaSalida = fechaFin, IdCabania = reserva.IdCabania, IdReserva = reserva.IdReservacion },
                                                                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Reserva>> GenerarReservaPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Reserva>("sp_GenerarReservaPorFecha",
                                                            new { FechaEntrada = fechaInicio, FechaSalida = fechaFin },
                                                            commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> ActualizarReservas(Reserva reserva)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("spActualizarReserva", new { reserva.Estado, reserva.IdReservacion }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Crear(Reserva modelo) // Una vez que se pasaron los datos se hace esta creacion
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("sp_CrearReserva",
                                                    new
                                                    {
                                                        modelo.NombreCliente,
                                                        modelo.FechaEntrada,
                                                        modelo.FechaSalida,
                                                        modelo.NumeroPersonas,
                                                        modelo.IdCabania
                                                    },
                                                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Editar(Reserva modelo) // Se supone que cuando se llegue a esta parte la cabaña tendria ya esas fechas libres
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("sp_EditarReserva",
                                                    new
                                                    {
                                                        modelo.IdReservacion,
                                                        modelo.NombreCliente,
                                                        modelo.FechaEntrada,
                                                        modelo.FechaSalida,
                                                        modelo.NumeroPersonas,
                                                        modelo.IdCabania,
                                                        modelo.Estado
                                                    },
                                                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                var reserva = await BuscadorId(id);

                if (reserva == null)
                {
                    throw new Exception("La reserva no existe.");
                }

                return await conexionD.ExecuteAsync("sp_EliminarReserva",
                                                    new { IdReservacion = id },
                                                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
