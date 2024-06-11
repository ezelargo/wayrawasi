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
                return await conexionD.QueryFirstOrDefaultAsync<Reserva>("SELECT * FROM Reservaciones WHERE IdReservacion = @Id", new { Id = id });
            }
        }

        public async Task<Cabania> BuscarPorIDCabania(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Cabania>("SELECT * FROM Cabanias WHERE IdCabania = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<Reserva>> ListarTodos()
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Reserva>("SELECT * FROM Reservaciones");
            }
        }

        public async Task<IEnumerable<Cabania>> ListarCabanias()
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Cabania>("SELECT * FROM Cabanias");
            }
        }


        /* 
            Funcionamiento: En caso de crear se ve si la cabaña esta ocupada en esas fechas, si no lo esta entonces se asigna la cabaña a la reserva y listo.
            En caso de editar se toma que la nueva cabaña ya filtrada no tenga otra reserva con la misma fecha o una fecha entre medio(debido a que estara ocuapada)
            donde si esto se cumple entonces la vieja fecha dejara de estar asignada a la reservacion por lo que automaticamente como una de las condiciones de conexion para
            este metodo no funciona(INNER JOIN) cuando se quiera asignar una nueva reserva a cabaña en las fechas liberas se podra hacer,
            mientras que la nueva cabaña ya estara asignada a esa fecha.
         */
        public async Task<Cabania> BuscarCabaniaDisponibilidad(int id,DateTime? fechaInicio, DateTime? fechaFin)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                // Esta consulta compara si la fecha de entrada coincide en el rango de fechas entre la entrada o la salida de alguna reserva y lo mismo para la Fecha de Salida. Teniendo asi que ni la entrada ni la salida se solapan con otra reserva.
                return await conexionD.QueryFirstOrDefaultAsync<Cabania>("SELECT * FROM Cabanias c INNER JOIN Reservaciones r ON r.IdCabania = c.IdCabania WHERE r.FechaEntrada BETWEEN @FechaEntrada AND @FechaSalida OR r.FechaSalida BETWEEN @FechaEntrada AND @FechaSalida", new { FechaEntrada = fechaInicio, FechaSalida = fechaFin });
            }
        }

        public async Task<int> Crear(Reserva modelo) // Una vez que se pasaron los datos se hace esta creacion
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("INSERT INTO Reservaciones (NombreCliente, FechaEntrada, FechaSalida, NumeroPersonas, IdCabania, CabaniaIdCabania, Estado) VALUES (@NombreCliente, @FechaEntrada, @FechaSalida, @NumeroPersonas, @IdCabania,@IdCabania, 'Reservado')", modelo);
            }
        }

        public async Task<int> Editar(Reserva modelo) // Se supone que cuando se llegue a esta parte la cabaña tendria ya esas fechas libres
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("UPDATE Reservaciones SET NombreCliente = @NombreCliente, FechaEntrada = @FechaEntrada, FechaSalida = @FechaSalida, NumeroPersonas = @NumeroPersonas, IdCabania = @IdCabania, CabaniaIdCabania = @IdCabania, Estado = @Estado WHERE IdReservacion = @IdReservacion", modelo);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                var reserva = await conexionD.QueryFirstOrDefaultAsync<Reserva>("SELECT * FROM Reservaciones WHERE IdReservacion = @Id", new { Id = id });

                if (reserva == null)
                {
                    throw new Exception("La reserva no existe.");
                }

                return await conexionD.ExecuteAsync("DELETE FROM Reservaciones WHERE IdReservacion = @Id", new { Id = id });
            }
        }
    }
}
