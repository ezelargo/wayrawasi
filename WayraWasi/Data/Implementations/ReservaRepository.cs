using Dapper;
using System.Data;
using WayraWasi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WayraWasi.Data.Implementations
{
    public class ReservaRepository : IReservasRepository
    {
        private readonly DBDapperContext _conexionDapper;
        private List<Cabania> _cabanias;

        public ReservaRepository(DBDapperContext conexionDapper)
        {
            _conexionDapper = conexionDapper;
            _cabanias = new List<Cabania>();
        }

        public async Task<Reserva> BuscadorId(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Reserva>("SELECT * FROM Reservaciones WHERE IdReservacion = @Id", new { Id = id });
            }
        }

        public async Task<Reserva> BuscarPorIDCabania(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Reserva>("SELECT * FROM Reservaciones WHERE IdCabania = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<Reserva>> ListarTodos()
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Reserva>("SELECT * FROM Reservaciones");
            }
        }

        public async Task<int> Crear(Reserva modelo)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                var cabaña = await conexionD.QueryFirstOrDefaultAsync<Cabania>("SELECT * FROM Cabanias WHERE IdCabania = @IdCabania AND Disponible = 1", new { IdCabania = modelo.IdCabania });

                if (cabaña == null)
                {
                    throw new Exception("La cabaña no está disponible para reservar.");
                }

                await conexionD.ExecuteAsync("UPDATE Cabanias SET Disponible = 0 WHERE IdCabania = @IdCabania", new { IdCabania = modelo.IdCabania });

                return await conexionD.ExecuteAsync("INSERT INTO Reservaciones (NombreCliente, FechaEntrada, FechaSalida, NumeroPersonas, IdCabania) VALUES (@NombreCliente, @FechaEntrada, @FechaSalida, @NumeroPersonas, @IdCabania)", modelo);
            }
        }

        public async Task<int> Editar(Reserva modelo)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                var cabañaAnterior = await conexionD.QueryFirstOrDefaultAsync<Cabania>("SELECT * FROM Cabanias WHERE IdCabania = @IdCabaniaAnterior", new { IdCabaniaAnterior = modelo.IdCabania });

                if (cabañaAnterior == null)
                {
                    throw new Exception("La cabaña anterior no existe.");
                }

                await conexionD.ExecuteAsync("UPDATE Cabanias SET Disponible = 1 WHERE IdCabania = @IdCabania", new { IdCabaniaAnterior = cabañaAnterior.IdCabania });

                await conexionD.ExecuteAsync("UPDATE Cabanias SET Disponible = 0 WHERE IdCabania = @IdCabania", new { IdCabania = modelo.IdCabania });
                
                return await conexionD.ExecuteAsync("UPDATE Reservaciones SET NombreCliente = @NombreCliente, FechaEntrada = @FechaEntrada, FechaSalida = @FechaSalida, NumeroPersonas = @NumeroPersonas, IdCabania = @IdCabania WHERE IdReservacion = @IdReservacion", modelo);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                var reserva = await conexionD.QueryFirstOrDefaultAsync<Reserva>("SELECT * FROM Reservaciones WHERE IdReserva = @IdReserva", new { IdReserva = id });

                if (reserva == null)
                {
                    throw new Exception("La reserva no existe.");
                }

                await conexionD.ExecuteAsync("UPDATE Cabanias SET Disponible = 1 WHERE IdCabania = @IdCabania", new { IdCabania = reserva.IdCabania });

                return await conexionD.ExecuteAsync("DELETE FROM Reservaciones WHERE IdReservacion = @Id", new { Id = id });
            }
        }
    }
}
