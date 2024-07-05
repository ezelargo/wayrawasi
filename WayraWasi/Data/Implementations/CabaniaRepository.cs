using System.Data;
using WayraWasi.Models;
using WayraWasi.Data;
using Dapper;

namespace WayraWasi.Data.Implementations
{
    public class CabaniaRepository : ICabaniasRepository
    {
        private readonly DBDapperContext _conexionDapper;

        public CabaniaRepository(DBDapperContext conexionDapper)
        {
            _conexionDapper = conexionDapper;
        }
        public async Task<Cabania> BuscadorId(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Cabania>("sp_BuscarCabaniaId",
                                                                          new { Id = id },
                                                                          commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<Reserva> BuscarReservaAsignadaACabania(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Reserva>("sp_BuscarReservaAsignadaACabania",
                                                                         new { Id = id },
                                                                         commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<Cabania> BuscarPorNombre(string nombre, int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryFirstOrDefaultAsync<Cabania>("sp_BuscarCabaniaPorNombre",
                                                                          new { NombreCabania = nombre, Id = id },
                                                                          commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Cabania>> ListarTodos()
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Cabania>("sp_ListarTodasCabanias", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Crear(Cabania modelo)
        {
            try
            {
                using (var conexionD = _conexionDapper.GetConnection())
                {
                    return await conexionD.ExecuteAsync("sp_CrearCabania",
                                                        new
                                                        {
                                                            modelo.NombreCabania,
                                                            modelo.Descripcion,
                                                            modelo.Capacidad,
                                                            modelo.PrecioNoche,
                                                            modelo.CheckIn,
                                                            modelo.CheckOut
                                                        }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<int> Editar(Cabania modelo)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("sp_EditarCabania",
            new
            {
                modelo.IdCabania,
                modelo.NombreCabania,
                modelo.Descripcion,
                modelo.Capacidad,
                modelo.PrecioNoche,
                modelo.CheckIn,
                modelo.CheckOut
            }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("sp_EliminarCabania",
                                                    new { IdCabania = id },
                                                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
