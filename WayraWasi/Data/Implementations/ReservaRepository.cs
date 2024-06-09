using Dapper;
using System.Data;
using WayraWasi.Models;

namespace WayraWasi.Data.Implementations
{
    public class ReservaRepository : IGenericRepository<Reserva, int>
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
                return await conexionD.QueryFirstOrDefaultAsync<Reserva>("SELECT * FROM Reservaciones WHERE IdReservacion = @IdReservacion", new { Id = id });
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
                return await conexionD.ExecuteAsync("INSERT INTO Reservaciones (NombreCliente, FechaEntrada, FechaSalida, NumeroPersonas, IdCabania) VALUES (@NombreCliente, @FechaEntrada, @FechaSalida, @NumeroPersonas, @IdCabania)", modelo);
            }
        }

        public async Task<int> Editar(Reserva modelo)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("UPDATE Reservaciones SET NombreCliente = @NombreCliente, FechaEntrada = @FechaEntrada, FechaSalida = @FechaSalida, NumeroPersonas = @NumeroPersonas, IdCabania = @IdCabania WHERE IdReservacion = @IdReservacion", modelo);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("DELETE FROM Reservaciones WHERE IdReservacion = @IdReservacion", new { Id = id });
            }
        }
    }
}
