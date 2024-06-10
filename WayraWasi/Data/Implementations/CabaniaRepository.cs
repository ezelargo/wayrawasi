﻿using System.Data;
using WayraWasi.Models;
using WayraWasi.Data;
using Dapper;

namespace WayraWasi.Data.Implementations
{
    public class CabaniaRepository : IGenericRepository<Cabania>
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
                return await conexionD.QueryFirstOrDefaultAsync<Cabania>("SELECT * FROM Cabanias WHERE IdCabania = @Id ", new { Id = id });
            }
        }

        public async Task<IEnumerable<Cabania>> ListarTodos()
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.QueryAsync<Cabania>("SELECT * FROM Cabanias");
            }
        }

        public async Task<int> Crear(Cabania modelo)
        {
            try
            {
                using (var conexionD = _conexionDapper.GetConnection())
                {
                    return await conexionD.ExecuteAsync("INSERT INTO Cabanias (NombreCabania, Descripcion, Capacidad, PrecioNoche, Disponible) VALUES (@NombreCabania, @Descripcion, @Capacidad, @PrecioNoche, 1)", modelo);
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<int> Editar(Cabania modelo)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("UPDATE Cabanias SET NombreCabania = @NombreCabania, Descripcion = @Descripcion, Capacidad = @Capacidad, PrecioNoche = @PrecioNoche, Disponible = @Disponible WHERE IdCabania = @IdCabania", modelo);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var conexionD = _conexionDapper.GetConnection())
            {
                return await conexionD.ExecuteAsync("DELETE FROM Cabanias WHERE IdCabania = @Id", new { Id = id });
            }
        }
    }
}
